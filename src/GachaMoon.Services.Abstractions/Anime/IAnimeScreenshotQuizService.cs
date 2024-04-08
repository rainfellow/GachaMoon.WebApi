namespace GachaMoon.Services.Abstractions.Anime;

public interface IAnimeScreenshotQuizService
{
    public Task<string> GenerateQuestion();
    public Task<bool> CheckAnswer(string question, string answer);
}
