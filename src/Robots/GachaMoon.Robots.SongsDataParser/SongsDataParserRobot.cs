using Ensersoft.Robots;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Time;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Domain.Animes;
using GachaMoon.Robots.SongsDataParser.Models;

namespace GachaMoon.Robots.SongsDataParser;

public class SongsDataParserRobot(
    IMyAnimeListApiClient userAnimeListClient,
    IAnimeClient animeClient,
    IAnisongDBClient anisongDBClient,
    IClockProvider clockProvider,
    ApplicationDbContext dbContext,
    ILogger<SongsDataParserRobot> logger) : IRobot
{
    private readonly IMyAnimeListApiClient _userAnimeListClient = userAnimeListClient;
    private readonly IAnimeClient _animeClient = animeClient;
    private readonly IClockProvider _clockProvider = clockProvider;
    private readonly IAnisongDBClient _anisongDBClient = anisongDBClient;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<SongsDataParserRobot> _logger = logger;

    public async Task RunJob()
    {
        //await ProcessJsonFile();
        await DownloadCatboxAudio();
        //await FixAnimeSongs();
        //await FixAnimes();
    }

    private async Task ProcessJsonFile()
    {
        using var stream = File.OpenRead($"E:/libraryMasterList.json");
        using var outputFile = new StreamWriter($"E:/failedAnimeSongs.txt");
        var jsonAnimeData = JsonSerializer.Deserialize<AMQJsonData>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? throw new NotImplementedException();

        var artists = await _dbContext.AnimeSongArtist.ToListAsync();
        foreach (var animeData in jsonAnimeData.AnimeMap)
        {
            if (animeData.Value.MainNames.JA == null || animeData.Value.MainNames.JA.Trim() == "")
            {
                if (animeData.Value.MainNames.EN == null || animeData.Value.MainNames.EN.Trim() == "")
                {
                    _logger.LogInformation("Skipping anime with no japanese name");
                    outputFile.WriteLine($"Skipping anime with no japanese name");
                    continue;
                }
                animeData.Value.MainNames.JA = animeData.Value.MainNames.EN;
            }
            var now = _clockProvider.Now;
            var animeExists = await _dbContext.Animes.AnyAsync(x => x.AnimeMusicQuizId == animeData.Value.AnnId);
            if (animeExists)
            {
                continue;
            }
            var songsData = await _anisongDBClient.GetAnimeSongsData(animeData.Value.MainNames.JA);
            if (songsData.Count == 0)
            {
                _logger.LogInformation("Failed to get songs data for {AnimeName}", animeData.Value.MainNames.JA);
                outputFile.WriteLine($"Failed to get songs data for {animeData.Value.MainNames.JA}");
                await Task.Delay(500);
                continue;
            }
            if (songsData.First().LinkedIds.Myanimelist == null)
            {
                _logger.LogInformation("Failed to process songs data for {AnimeName}. No mal id!", animeData.Value.MainNames.JA);
                outputFile.WriteLine($"Failed to process songs data for {animeData.Value.MainNames.JA}. No mal id!");
                continue;
            }
            var malId = songsData.First().LinkedIds.Myanimelist;
            var anime = await _dbContext.Animes.FirstOrDefaultAsync(x => x.AnimeBaseId == malId);
            if (anime is null)
            {
                var malAnime = await _userAnimeListClient.GetAnimeDetails(malId!.Value);
                if (malAnime == null || malAnime.Title == null)
                {
                    _logger.LogInformation("No MAL Entry for {AnimeName}", animeData.Value.MainNames.JA);
                    outputFile.WriteLine($"No MAL Entry for {animeData.Value.MainNames.JA}");
                    await Task.Delay(1000);
                    continue;
                }
                anime = new Anime
                {
                    AnimeBaseId = malAnime.MalId,
                    Title = malAnime.Title,
                    ImageSiteTitle = null,
                    AgeRating = malAnime.AgeRating,
                    AnimeType = malAnime.AnimeType,
                    EpisodeCount = malAnime.EpisodeCount,
                    MeanScore = malAnime.MeanScore,
                    StartDate = malAnime.StartDate,
                    AnilistId = songsData.First().LinkedIds.Anilist ?? 0,
                    HasImages = false,
                    HasSongs = false,
                    CreatedAt = now
                };
                await _dbContext.Animes.AddAsync(anime);
                await _dbContext.SaveChangesAsync();
            }
            anime.AnimeMusicQuizId = animeData.Value.AnnId;
            anime.HasSongs = true;
            anime.AnilistId = songsData.First().LinkedIds.Anilist ?? anime.AnilistId;
            anime.UpdatedAt = now;
            var newSongs = songsData.Select(x => new AnimeSong
            {
                AnimeId = anime.Id,
                SongName = x.SongName,
                AMQSongCategory = x.SongCategory ?? "No Category",
                AMQSongDifficulty = x.SongDifficulty ?? 0,
                CatboxAudioLink = x.Audio,
                CatboxHQLink = x.HQ,
                CatboxMQLink = x.MQ,
                SongTypeString = x.SongType,
                AnimeSongArtists = x.Artists.Select(y =>
                {
                    var existing = artists.FirstOrDefault(x => x.ArtistId == y.Id);
                    if (existing is not null)
                    {
                        return existing;
                    }
                    var newArtist = new AnimeSongArtist
                    {
                        ArtistName = y.Names.First(),
                        ArtistType = AnimeSongArtistType.Performer,
                        ArtistId = y.Id
                    };
                    artists.Add(newArtist);
                    return newArtist;
                }).ToList()
            }).ToList();
            await _dbContext.AddRangeAsync(newSongs);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Added anime {AnimeName} successfully", animeData.Value.MainNames.JA);
            await Task.Delay(1200);
        }
    }

    private async Task FixAnimes()
    {
        var counter = 0;
        var animes = await _dbContext.Animes
            .Where(x => !x.AnimeSongs.Any())
            .ToListAsync();
        foreach (var anime in animes)
        {
            anime.HasSongs = false;
            counter++;
            anime.UpdatedAt = _clockProvider.Now;
        }
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Fixed {Counter} animes", counter);
    }

    private async Task FixAnimeSongs()
    {
        var counter = 0;
        var animeSongs = await _dbContext.AnimeSong
            .Where(x => x.SongType == SongType.None)
            .ToListAsync();
        foreach (var song in animeSongs)
        {
            if (song.SongTypeString is null)
            {
                counter++;
                continue;
            }
            if (song.SongTypeString.Contains("Opening"))
            {
                song.SongType = SongType.Opening;
            }
            if (song.SongTypeString.Contains("Ending"))
            {
                song.SongType = SongType.Ending;
            }
            if (song.SongTypeString.Contains("Insert"))
            {
                song.SongType = SongType.Insert;
            }
            song.UpdatedAt = _clockProvider.Now;
        }
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("{Counter} songs were not fixed", counter);
    }

    private async Task DownloadCatboxAudio()
    {
        var animeSongs = await _dbContext.AnimeSong.Where(x => x.CatboxAudioLink != null).ToListAsync();
        var tasks = new List<Task>();
        foreach (var animeSong in animeSongs)
        {
            var audioFile = new FileInfo(Path.Combine("F:/catbox_audio", $"{animeSong.CatboxAudioLink}"));
            if (File.Exists(audioFile.FullName))
            {
                _logger.LogInformation("Already downloaded catbox audio for {SongName}", animeSong.SongName);
                continue;
            }
            else
            {
                try
                {
                    await DownloadFileAsync(animeSong.CatboxAudioLink!);
                    _logger.LogInformation("Downloaded catbox audio for {SongName}", animeSong.SongName);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                    continue;
                }
            }
        }
    }

    private async Task DownloadFileAsync(string filename)
    {
        using var ph = new HttpClientHandler();
        using var client = new HttpClient(ph) { Timeout = Timeout.InfiniteTimeSpan };

        using var hm = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://files.catbox.moe/{filename}"));
        using var netstream = await client.GetStreamAsync(hm.RequestUri);
        using var filestream = new FileStream($"F:/catbox_audio/{filename}", FileMode.Create);
        await netstream.CopyToAsync(filestream);
    }
}