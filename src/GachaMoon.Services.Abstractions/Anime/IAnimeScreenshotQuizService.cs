using GachaMoon.Services.Abstractions.Anime.Data;

namespace GachaMoon.Services.Abstractions.Anime;

public interface IAnimeScreenshotQuizService
{
    public Task<string> GenerateQuestion(UserAnimeListData? userList = null);
    public Task<ICollection<QuizQuestionData>> GenerateQuiz(QuizConfigData config, ICollection<UserAnimeListData>? userLists = null);
    public Task<QuizAnswerData> CheckAnswer(string question, string answer);
}
