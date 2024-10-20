using Ensersoft.Robots;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Time;
using GachaMoon.Services.Anime.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Domain.Animes;
using GachaMoon.Common.Query;
using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Robots.ScreenshotDataParser;

public class ScreenshotDataParserRobot(
    IMyAnimeListApiClient userAnimeListClient,
    IAnimeClient animeClient,
    IClockProvider clockProvider,
    ApplicationDbContext dbContext,
    ILogger<ScreenshotDataParserRobot> logger) : IRobot
{
    private readonly IMyAnimeListApiClient _userAnimeListClient = userAnimeListClient;
    private readonly IAnimeClient _animeClient = animeClient;
    private readonly IClockProvider _clockProvider = clockProvider;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<ScreenshotDataParserRobot> _logger = logger;
    //private static readonly List<string> Letters = ["0", "a", "b", "e", "f"];
    //private static readonly List<string> Letters = ["g", "j", "k", "l"];
    //private static readonly List<string> Letters = ["m", "n", "o", "p"];
    //private static readonly List<string> Letters = ["q", "r", "u", "v"];
    private static readonly List<string> Letters = ["t"];

    public async Task RunJob()
    {
        //await UpdateExistingAnimes();
        foreach (var letter in Letters)
        {
            await ProcessJsonFile(letter);
        }
    }

    private async Task ProcessJsonFile(string letter)
    {
        using var stream = File.OpenRead($"E:/UltimateDatasetCreate-master/animeScreenshotData_{letter}.json");
        using var outputFile = new StreamWriter($"E:/UltimateDatasetCreate-master/failedAnime.txt");
        var jsonAnimeData = JsonSerializer.Deserialize<ICollection<JsonAnimeData>>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? throw new NotImplementedException();
        foreach (var animeData in jsonAnimeData)
        {
            var clientAnime = await TryGetCorrectAnimeData(animeData.Name);
            var malString = clientAnime.Sources.FirstOrDefault(x => x.Contains("myanimelist.net/anime/"));
            var toBeSearched = "myanimelist.net/anime/";
            var ix = malString!.IndexOf(toBeSearched);
            var animeIdString = ix != -1 ? malString[(ix + toBeSearched.Length)..] : throw new NotImplementedException();
            var animeId = int.Parse(animeIdString);
            var malAnime = await _userAnimeListClient.GetAnimeDetails(animeId);
            var existingAnime = await _dbContext.Animes.FirstOrDefaultAsync(x => x.AnimeBaseId == malAnime.MalId);
            if (existingAnime == null)
            {
                var anime = new Anime
                {
                    AnimeBaseId = malAnime.MalId,
                    Title = malAnime.Title,
                    ImageSiteTitle = animeData.Name,
                    AgeRating = malAnime.AgeRating,
                    AnimeType = malAnime.AnimeType,
                    EpisodeCount = malAnime.EpisodeCount,
                    MeanScore = malAnime.MeanScore,
                    StartDate = malAnime.StartDate,
                    HasImages = true,
                    HasSongs = false,
                    AnilistId = 0,
                    AnimeEpisodes = CreateAnimeEpisodes(animeData.Episodes)
                };
                await _dbContext.AddAsync(anime);
                await _dbContext.SaveChangesAsync();
            }
            else if (existingAnime.HasImages)
            {
                outputFile.WriteLine($"Anime already exists: {animeData.Name}, mal id {malAnime.MalId} mal name {malAnime.Title}");
                continue;
            }
            else
            {
                existingAnime.HasImages = true;
                existingAnime.AnimeEpisodes = CreateAnimeEpisodes(animeData.Episodes);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task<AnimeData> TryGetCorrectAnimeData(string name)
    {
        var animeClientData = await _animeClient.AnimeFromQuery(name);
        var clientAnime = animeClientData.FirstOrDefault(x => x.Synonyms.Any(y => y.ToLower().Replace(" ", string.Empty) == name.ToLower().Replace(" ", string.Empty)) || x.Title.ToLower() == name.ToLower()) ?? animeClientData.First();
        if (clientAnime == default)
        {
            logger.LogError("wtf");
            throw new NotImplementedException();
        }
        return clientAnime;
    }

    private async Task UpdateExistingAnimes()
    {
        var animesToUpdate = await _dbContext.Animes
            .IsNotDeleted()
            .Include(x => x.AnimeEpisodes)
            .Where(x => x.AnilistId == 0)
            .ToListAsync();
        foreach (var anime in animesToUpdate)
        {
            var animeData = await _animeClient.AnimeFromId(anime.AnimeBaseId);
            anime.AnilistId = int.Parse(animeData.ProviderMapping.Anilist ?? "0");
            await _dbContext.SaveChangesAsync();
            await Task.Delay(800);
        }
    }

    private static ICollection<AnimeEpisode> CreateAnimeEpisodes(ICollection<JsonAnimeEpisodeData> jsonEpisodes)
    {
        return jsonEpisodes
            .Select((x, i) => new AnimeEpisode
            {
                Title = x.Name,
                AnimeImages = CreateAnimeImages(x)
            })
            .Where(x => x.AnimeImages.Count > 0)
            .Select((x, i) =>
            {
                x.EpisodeNumber = i + 1;
                return x;
            })
            .ToList();
    }

    private static ICollection<AnimeImage> CreateAnimeImages(JsonAnimeEpisodeData jsonEpisode)
    {
        return jsonEpisode.Images.Select(x => new AnimeImage
        {
            Url = x.Url,
            SourceImageUrl = x.Url.Replace("animethumbs.fancaps.net/", "cdni.fancaps.net/file/fancaps-animeimages/"),
            VoteCount = 0,
            BadVoteCount = 0,
            VoteSum = 0
        }).ToList();
    }
}