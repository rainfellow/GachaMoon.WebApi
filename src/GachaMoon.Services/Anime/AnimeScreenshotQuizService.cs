using System.Globalization;
using GachaMoon.Database;
using GachaMoon.Domain.Animes;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Anime.Data;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Time;
using GachaMoon.Services.Anime.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserAnimeListData = GachaMoon.Services.Abstractions.Anime.Data.UserAnimeListData;

namespace GachaMoon.Services.Anime;

public class AnimeScreenshotQuizService : IAnimeScreenshotQuizService
{
    private readonly IClockProvider _clockProvider;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Dictionary<string, CachedAnimeData> QuizDictionary { get; set; } = new Dictionary<string, CachedAnimeData>();
    private List<CachedAnimeData> AvailableAnimes { get; set; } = new List<CachedAnimeData>();
    private List<CachedAnimeData> AvailableImageAnimes { get; set; } = new List<CachedAnimeData>();
    private List<CachedAnimeData> AvailableSongAnimes { get; set; } = new List<CachedAnimeData>();

    public AnimeScreenshotQuizService(IServiceScopeFactory serviceScopeFactory, IClockProvider clockProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _clockProvider = clockProvider;
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        AvailableAnimes = [.. dbContext.Animes.Select(x => new CachedAnimeData
        {
            MyAnimeListId = x.AnimeBaseId,
            AnilistId = x.AnilistId,
            Title = x.Title,
            InternalId = x.Id,
            AgeRating = x.AgeRating,
            StartDate = FetchDateFromString(x.StartDate),
            AnimeType = x.AnimeType,
            MeanScore = x.MeanScore,
            HasImages = x.HasImages,
            HasSongs = x.HasSongs,
            HasOps = x.AnimeSongs.Any(x => x.SongType == SongType.Opening),
            HasEds = x.AnimeSongs.Any(x => x.SongType == SongType.Ending),
            HasIns = x.AnimeSongs.Any(x => x.SongType == SongType.Insert),
        })];
        AvailableImageAnimes = AvailableAnimes.Where(x => x.HasImages).ToList();
        AvailableSongAnimes = AvailableAnimes.Where(x => x.HasSongs).ToList();
    }

    private static DateOnly FetchDateFromString(string dateString)
    {
        if (dateString == "ERR")
        {
            return DateOnly.Parse("1970-01-01");
        }

        var goodString = DateOnly.TryParse(dateString, CultureInfo.InvariantCulture, out var result);
        if (!goodString)
        {
            var goodYear = int.TryParse(dateString[..4], out var year);
            if (!goodYear)
            {
                return DateOnly.Parse("1970-01-01");
            }
            return new DateOnly(year, 1, 1);
        }

        return result;
    }

    public async Task<string> GenerateQuestion(UserAnimeListData? userList = null)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<QuizQuestionData>> GenerateQuiz(QuizConfigData config, ICollection<UserAnimeListData>? userLists = null)
    {
        var result = new List<QuizQuestionData>();
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userList = CombineUserAnimeLists(userLists);

        if (config.ImageQuestions > 0)
        {
            var allowedTypes = GetAllowedAnimeTypes(config);
            var eligibleImageAnime = (userList == null || userList.UserAnimes == null || userList.UserAnimes.Count == 0)
            ? AvailableImageAnimes.Where(x => allowedTypes.Contains(x.AnimeType)).ToList()
            : AvailableImageAnimes.Where(x => allowedTypes.Contains(x.AnimeType)).IntersectBy(userList.UserAnimes.Select(x => x.Id), y => y.InternalId).ToList();

            if (!eligibleImageAnime.Any())
            {
                eligibleImageAnime = AvailableImageAnimes;
            }
            var filteredEligibleAnime = eligibleImageAnime
                .Where(x => x.MeanScore >= config.MinRating && x.MeanScore <= config.MaxRating)
                .Where(x => x.StartDate.Year >= config.MinReleaseYear && x.StartDate.Year <= config.MaxReleaseYear)
                .ToList();
            if (!filteredEligibleAnime.Any())
            {
                filteredEligibleAnime = eligibleImageAnime;
            }
            var chosenQuizAnimes = GenerateQuizAnimeList(filteredEligibleAnime, config, config.ImageQuestions);
            var rand = new Random((int)_clockProvider.Now.ToUnixTimeMilliseconds());
            foreach (var selectedAnime in chosenQuizAnimes)
            {
                var imgs = dbContext.AnimeImages.Where(x => x.AnimeEpisode.Anime.Id == selectedAnime.InternalId);
                var skipper = rand.Next(0, imgs.Count() - 1);
                var selectedImage = await imgs.Skip(skipper).Take(1).Select(x => new { x.SourceImageUrl, FromEpisode = x.AnimeEpisode.EpisodeNumber }).FirstOrDefaultAsync() ?? throw new NotImplementedException();
                result.Add(new QuizQuestionData
                {
                    Question = selectedImage.SourceImageUrl,
                    Answer = selectedAnime.InternalId,
                    FromEpisode = selectedImage.FromEpisode,
                    QuestionType = QuizQuestionType.Image
                });
            }
        }

        if (config.SongQuestions > 0)
        {
            var allowedTypes = GetAllowedAnimeTypes(config);
            var eligibleSongAnime = (userList == null || userList.UserAnimes == null || userList.UserAnimes.Count == 0)
            ? AvailableSongAnimes.Where(x => allowedTypes.Contains(x.AnimeType)).ToList()
            : AvailableSongAnimes.Where(x => allowedTypes.Contains(x.AnimeType)).IntersectBy(userList.UserAnimes.Select(x => x.Id), y => y.InternalId).ToList();

            if (!eligibleSongAnime.Any())
            {
                eligibleSongAnime = AvailableSongAnimes;
            }
            var allowedSongTypes = GetAllowedSongTypes(config);
            var filteredEligibleAnime = eligibleSongAnime
                .Where(x => x.MeanScore >= config.MinRating && x.MeanScore <= config.MaxRating)
                .Where(x => x.StartDate.Year >= config.MinReleaseYear && x.StartDate.Year <= config.MaxReleaseYear)
                .Where(x => (config.AllowOps && x.HasOps) || (config.AllowEds && x.HasEds) || (config.AllowIns && x.HasIns))
                .ToList();
            if (!filteredEligibleAnime.Any())
            {
                filteredEligibleAnime = eligibleSongAnime;
            }
            var chosenQuizAnimes = GenerateQuizAnimeList(filteredEligibleAnime, config, config.SongQuestions);
            var rand = new Random((int)_clockProvider.Now.ToUnixTimeMilliseconds());
            var selectedSongs = new List<string>();

            var skippedAnime = 0;
            foreach (var selectedAnime in chosenQuizAnimes)
            {
                var songs = dbContext.AnimeSong
                    .Where(x => x.AnimeId == selectedAnime.InternalId && x.CatboxAudioLink != null && !selectedSongs.Any(y => y == x.CatboxAudioLink))
                    .Where(x => allowedSongTypes.Contains(x.SongType));
                var skipper = rand.Next(0, songs.Count() - 1);
                var selectedSong = await songs.Skip(skipper).Take(1).Select(x => new { x.CatboxAudioLink }).FirstOrDefaultAsync();
                if (selectedSong == null)
                {
                    skippedAnime++;
                    continue;
                }
                result.Add(new QuizQuestionData
                {
                    Question = selectedSong.CatboxAudioLink!,
                    Answer = selectedAnime.InternalId,
                    FromEpisode = 0,
                    QuestionType = QuizQuestionType.Song
                });
                selectedSongs.Add(selectedSong.CatboxAudioLink!);
            }
        }
        return result;
    }

    public async Task<QuizAnswerData> CheckAnswer(string question, string answer)
    {
        QuizDictionary.Remove(question, out var correctAnswer);
        if (correctAnswer == null)
        {
            throw new NotImplementedException();
        }

        using var scope = _serviceScopeFactory.CreateScope();
        var animequizClient =
            scope.ServiceProvider.GetRequiredService<IAnimeQuizClient>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AnimeScreenshotQuizService>>();
        var result = await animequizClient.GetQuizResult(answer);
        return new QuizAnswerData(correctAnswer.Title,
            result.AnimeTitle,
            correctAnswer.InternalId == result.AnimeId);
    }

    private UserAnimeListData? CombineUserAnimeLists(ICollection<UserAnimeListData>? userLists)
    {
        if (userLists == null || userLists.Count == 0)
        {
            return null;
        }

        return new UserAnimeListData
        {
            UserAnimes = userLists
                .SelectMany(x => x.UserAnimes)
                .DistinctBy(x => x.Id)
                .ToList()
        };
    }

    private ICollection<CachedAnimeData> GenerateQuizAnimeList(ICollection<CachedAnimeData> animeList, QuizConfigData config, int number)
    {
        var rand = new Random((int)_clockProvider.Now.ToUnixTimeMilliseconds());
        var shuffledList = animeList.OrderBy(x => rand.Next()).ToList();
        var result = new List<CachedAnimeData>();
        if (config.DiversifyAnime)
        {
            while (result.Count < number)
            {
                result.AddRange(shuffledList.Take(number - result.Count).ToList());
            }
            result = [.. result.OrderBy(x => rand.Next())];
        }
        else
        {
            result = Enumerable.Range(0, number)
                .Select(_ => shuffledList.ElementAt(rand.Next(shuffledList.Count())))
                .ToList();
        }
        return result;
    }
    private List<SongType> GetAllowedSongTypes(QuizConfigData configData)
    {
        var result = new List<SongType>();
        if (configData.AllowOps)
        {
            result.Add(SongType.Opening);
        }
        if (configData.AllowEds)
        {
            result.Add(SongType.Ending);
        }
        if (configData.AllowIns)
        {
            result.Add(SongType.Insert);
        }
        if (!result.Any())
        {
            result.Add(SongType.Opening);
        }
        return result;
    }

    private List<string> GetAllowedAnimeTypes(QuizConfigData configData)
    {
        var result = new List<string>();
        if (configData.AllowTv)
        {
            result.Add("tv");
        }
        if (configData.AllowOva)
        {
            result.Add("ova");
            result.Add("ona");
        }
        if (configData.AllowMovie)
        {
            result.Add("movie");
        }
        if (configData.AllowSpecial)
        {
            result.Add("special");
            result.Add("tv_special");
        }
        if (configData.AllowMusic)
        {
            result.Add("music");
        }
        if (!result.Any())
        {
            result.Add("tv");
        }
        return result;
    }
}
