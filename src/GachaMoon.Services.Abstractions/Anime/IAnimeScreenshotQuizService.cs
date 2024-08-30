using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Anime.Data;

namespace GachaMoon.Services.Abstractions.Anime;

public interface IAnimeScreenshotQuizService
{
    public Task<string> GenerateQuestion(UserAnimeListData? userList = null);
    public Task<QuizAnswerData> CheckAnswer(string question, string answer);
}
