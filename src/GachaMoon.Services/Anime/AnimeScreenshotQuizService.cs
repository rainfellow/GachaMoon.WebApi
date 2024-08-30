using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Anime.Data;
using GachaMoon.Services.Abstractions.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Services.Anime;

public class AnimeScreenshotQuizService : IAnimeScreenshotQuizService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Dictionary<string, UserAnimeData> QuizDictionary { get; set; } = new Dictionary<string, UserAnimeData>();
    private List<UserAnimeData> AvailableAnimes { get; set; } = new List<UserAnimeData>();

    public AnimeScreenshotQuizService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        AvailableAnimes = [.. dbContext.Animes.Select(x => new UserAnimeData { Id = x.AnimeBaseId, Title = x.Title })];
    }

    public async Task<string> GenerateQuestion(UserAnimeListData? userList = null)
    {
        var eligibleAnime = (userList == null || userList.UserAnimes == null || userList.UserAnimes.Count == 0) ? AvailableAnimes : userList.UserAnimes.IntersectBy(AvailableAnimes.Select(x => x.Id), y => y.Id).ToList();
        if (!eligibleAnime.Any())
        {
            eligibleAnime = AvailableAnimes;
        }
        var rand = new Random();
        var randomAnime = rand.Next(0, eligibleAnime.Count);
        var selectedAnime = eligibleAnime.ElementAt(randomAnime);
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var imgs = dbContext.AnimeImages.Where(x => x.AnimeEpisode.Anime.AnimeBaseId == selectedAnime.Id);
        var skipper = rand.Next(0, imgs.Count() - 1);
        var selectedImage = await imgs.Skip(skipper).Take(1).Select(x => new { x.Url }).FirstOrDefaultAsync() ?? throw new NotImplementedException();
        QuizDictionary.TryAdd(selectedImage.Url, selectedAnime);
        return selectedImage.Url;
    }

    public async Task<QuizAnswerData> CheckAnswer(string question, string answer)
    {
        QuizDictionary.Remove(question, out var correctAnswer);
        if (correctAnswer == null)
        {
            throw new NotImplementedException();
        }

        using var scope = _serviceScopeFactory.CreateScope();
        var animeClient =
            scope.ServiceProvider.GetRequiredService<IAnimeClient>();
        var possibleAnimes = await animeClient.AnimeFromQuery(answer);
        var answerAnime = possibleAnimes.First();
        return new QuizAnswerData(correctAnswer.Title,
            correctAnswer.Title.ToLowerInvariant() == answer.ToLowerInvariant()
            || correctAnswer.Title.ToLowerInvariant() == answerAnime.Title.ToLowerInvariant()
            || answerAnime.Synonyms.Any(x => x.ToLowerInvariant() == correctAnswer.Title.ToLowerInvariant()));
    }
}
