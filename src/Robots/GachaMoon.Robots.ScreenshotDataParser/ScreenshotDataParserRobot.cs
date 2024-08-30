using System.Globalization;
using Ensersoft.Robots;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Time;
using GachaMoon.Services.Anime.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Domain.Animes;

namespace GachaMoon.Robots.ScreenshotDataParser;

public class ScreenshotDataParserRobot(
    IUserAnimeListClient userAnimeListClient,
    IAnimeClient animeClient,
    IClockProvider clockProvider,
    ApplicationDbContext dbContext,
    ILogger<ScreenshotDataParserRobot> logger) : IRobot
{
    private readonly IUserAnimeListClient _userAnimeListClient = userAnimeListClient;
    private readonly IAnimeClient _animeClient = animeClient;
    private readonly IClockProvider _clockProvider = clockProvider;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<ScreenshotDataParserRobot> _logger = logger;
    //private static readonly List<string> Letters = ["0", "a", "b", "e", "f"];
    private static readonly List<string> Letters = ["g", "j"];

    private static DateOnly StartDate { get; }
        = DateOnly.FromDateTime(DateTime.ParseExact("2013-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture));

    public async Task RunJob()
    {
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
            var animeClientData = await _animeClient.AnimeFromQuery(animeData.Name);
            var clientAnime = animeClientData.First();
            var malString = clientAnime.Sources.FirstOrDefault(x => x.Contains("myanimelist.net/anime/"));
            var toBeSearched = "myanimelist.net/anime/";
            var ix = malString!.IndexOf(toBeSearched);
            var animeIdString = ix != -1 ? malString[(ix + toBeSearched.Length)..] : throw new NotImplementedException();
            var animeId = int.Parse(animeIdString);
            var malAnime = await _userAnimeListClient.GetAnimeDetails(animeId);
            var checkExisting = await _dbContext.Animes.AnyAsync(x => x.AnimeBaseId == malAnime.Id);
            if (checkExisting)
            {
                outputFile.WriteLine($"Anime already exists: {animeData.Name}, mal id {malAnime.Id} mal name {malAnime.Title}");
                continue;
            }
            var anime = new Anime
            {
                AnimeBaseId = malAnime.Id,
                Title = malAnime.Title,
                ImageSiteTitle = animeData.Name,
                AnimeEpisodes = CreateAnimeEpisodes(animeData.Episodes)
            };
            await _dbContext.AddAsync(anime);
            await _dbContext.SaveChangesAsync();
        }
    }

    private static ICollection<AnimeEpisode> CreateAnimeEpisodes(ICollection<JsonAnimeEpisodeData> jsonEpisodes)
    {
        return jsonEpisodes.Select(x => new AnimeEpisode
        {
            Title = x.Name,
            AnimeImages = CreateAnimeImages(x)
        }).ToList();
    }

    private static ICollection<AnimeImage> CreateAnimeImages(JsonAnimeEpisodeData jsonEpisode)
    {
        return jsonEpisode.Images.Select(x => new AnimeImage
        {
            Url = x.Url,
            VoteCount = 0,
            BadVoteCount = 0,
            VoteSum = 0
        }).ToList();
    }
}