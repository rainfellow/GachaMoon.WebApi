using System.Globalization;
using GachaMoon.Database;
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
    private Dictionary<int, long> AnimeMALIdToInternalIdDictionary { get; set; } = new Dictionary<int, long>();

    public AnimeScreenshotQuizService(IServiceScopeFactory serviceScopeFactory, IClockProvider clockProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _clockProvider = clockProvider;
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        AvailableAnimes = [.. dbContext.Animes.Select(x => new CachedAnimeData
        {
            Id = x.AnimeBaseId,
            Title = x.Title,
            InternalId = x.Id,
            AgeRating = x.AgeRating,
            StartDate = FetchDateFromString(x.StartDate),
            AnimeType = x.AnimeType,
            MeanScore = x.MeanScore,
        })];
        foreach (var anime in AvailableAnimes)
        {
            AnimeMALIdToInternalIdDictionary.TryAdd(anime.Id, anime.InternalId);
        }
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
        var eligibleAnime = (userList == null || userList.UserAnimes == null || userList.UserAnimes.Count == 0) ? AvailableAnimes : AvailableAnimes.IntersectBy(userList.UserAnimes.Select(x => x.Id), y => y.Id).ToList();
        if (!eligibleAnime.Any())
        {
            eligibleAnime = AvailableAnimes;
        }
        var rand = new Random((int)_clockProvider.Now.ToUnixTimeMilliseconds());
        var randomAnime = rand.Next(0, eligibleAnime.Count);
        var selectedAnime = eligibleAnime.ElementAt(randomAnime);
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var imgs = dbContext.AnimeImages.Where(x => x.AnimeEpisode.Anime.AnimeBaseId == selectedAnime.Id);
        var skipper = rand.Next(0, imgs.Count() - 1);
        var selectedImage = await imgs.Skip(skipper).Take(1).Select(x => new { x.SourceImageUrl }).FirstOrDefaultAsync() ?? throw new NotImplementedException();
        QuizDictionary.TryAdd(selectedImage.SourceImageUrl, selectedAnime);
        return selectedImage.SourceImageUrl;
    }

    public async Task<ICollection<QuizQuestionData>> GenerateQuiz(QuizConfigData config, ICollection<UserAnimeListData>? userLists = null)
    {
        var userList = CombineUserAnimeLists(userLists);
        var eligibleAnime = (userList == null || userList.UserAnimes == null || userList.UserAnimes.Count == 0) ? AvailableAnimes : AvailableAnimes.IntersectBy(userList.UserAnimes.Select(x => x.Id), y => y.Id).ToList();
        if (!eligibleAnime.Any())
        {
            eligibleAnime = AvailableAnimes;
        }
        var filteredEligibleAnime = eligibleAnime
            .Where(x => x.MeanScore >= config.MinRating && x.MeanScore <= config.MaxRating)
            .Where(x => x.StartDate.Year >= config.MinReleaseYear && x.StartDate.Year <= config.MaxReleaseYear)
            .ToList();
        if (!filteredEligibleAnime.Any())
        {
            filteredEligibleAnime = eligibleAnime;
        }
        var chosenQuizAnimes = GenerateQuizAnimeList(filteredEligibleAnime, config);
        var rand = new Random((int)_clockProvider.Now.ToUnixTimeMilliseconds());
        var result = new List<QuizQuestionData>();
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        foreach (var selectedAnime in chosenQuizAnimes)
        {
            var imgs = dbContext.AnimeImages.Where(x => x.AnimeEpisode.Anime.AnimeBaseId == selectedAnime.Id);
            var skipper = rand.Next(0, imgs.Count() - 1);
            var selectedImage = await imgs.Skip(skipper).Take(1).Select(x => new { x.SourceImageUrl, FromEpisode = x.AnimeEpisode.EpisodeNumber }).FirstOrDefaultAsync() ?? throw new NotImplementedException();
            result.Add(new QuizQuestionData
            {
                Question = selectedImage.SourceImageUrl,
                Answer = selectedAnime.InternalId,
                FromEpisode = selectedImage.FromEpisode
            });
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
            AnimeMALIdToInternalIdDictionary[correctAnswer.Id] == result.AnimeId);
    }

    private UserAnimeListData? CombineUserAnimeLists(ICollection<UserAnimeListData>? userLists)
    {
        if (userLists == null || userLists.Count == 0)
        {
            return null;
        }

        return new UserAnimeListData
        {
            UserAnimes = userLists.SelectMany(x => x.UserAnimes).DistinctBy(x => x.Id).ToList()
        };
    }

    private ICollection<CachedAnimeData> GenerateQuizAnimeList(ICollection<CachedAnimeData> animeList, QuizConfigData config)
    {
        var rand = new Random((int)_clockProvider.Now.ToUnixTimeMilliseconds());
        var shuffledList = animeList.OrderBy(x => rand.Next()).ToList();
        var result = new List<CachedAnimeData>();
        if (config.DiversifyAnime)
        {
            while (result.Count < config.QuestionNumber)
            {
                result.AddRange(shuffledList.Take(config.QuestionNumber - result.Count).ToList());
            }
            result = [.. result.OrderBy(x => rand.Next())];
        }
        else
        {
            result = Enumerable.Range(0, config.QuestionNumber)
                .Select(_ => shuffledList.ElementAt(rand.Next(shuffledList.Count())))
                .ToList();
        }
        return result;
    }
}
