using System.Reflection;
using System.Text.Json;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Anime.Data;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Anime.Data;
using UserAnimeListData = GachaMoon.Services.Abstractions.Anime.Data.UserAnimeListData;

namespace GachaMoon.Services.Anime;

public class JsonAnimeScreenshotQuizService : IAnimeScreenshotQuizService
{
    private readonly IAnimeClient _animeListClient;
    private static readonly string AssemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(AnimeScreenshotQuizService))?.Location)!;
    private ICollection<JsonAnimeData> JsonAnimeData { get; set; }
    private Dictionary<string, string> QuizDictionary { get; set; }
    public JsonAnimeScreenshotQuizService(IAnimeClient animeListClient)
    {
        _animeListClient = animeListClient;
        using var stream = File.OpenRead(Path.Join(AssemblyPath, "Static", "screenshotQuizData.json"));
        JsonAnimeData = JsonSerializer.Deserialize<ICollection<JsonAnimeData>>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? throw new NotImplementedException();
        QuizDictionary = new Dictionary<string, string>();
    }
    public Task<string> GenerateQuestion(UserAnimeListData? userList = null)
    {
        var rand = new Random();
        var randomAnime = rand.Next(0, JsonAnimeData.Count);
        var selectedAnime = JsonAnimeData.ElementAt(randomAnime);
        var selectedAnimeEpisode = selectedAnime.Episodes.ElementAt(rand.Next(0, selectedAnime.Episodes.Count));
        var selectedImage = selectedAnimeEpisode.Images.ElementAt(rand.Next(0, selectedAnimeEpisode.Images.Count));
        QuizDictionary.TryAdd(selectedImage.Url, selectedAnime.Name);
        return Task.FromResult(selectedImage.Url);
    }

    public async Task<QuizAnswerData> CheckAnswer(string question, string answer)
    {
        var correctAnswer = QuizDictionary[question];
        var possibleAnimes = await _animeListClient.AnimeFromQuery(answer);
        var answerAnime = possibleAnimes.First();
        return new QuizAnswerData(correctAnswer, answerAnime.Title, correctAnswer.ToLowerInvariant() == answerAnime.Title.ToLowerInvariant() || answerAnime.Synonyms.Any(x => x.ToLowerInvariant() == correctAnswer.ToLowerInvariant()));
    }

    public Task<ICollection<QuizQuestionData>> GenerateQuiz(QuizConfigData config, ICollection<UserAnimeListData>? userLists = null)
    {
        throw new NotImplementedException();
    }
}
