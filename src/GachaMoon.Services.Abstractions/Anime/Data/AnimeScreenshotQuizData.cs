namespace GachaMoon.Services.Abstractions.Anime.Data;
public record AnimeScreenshotQuizData
{
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;

    public AnimeScreenshotQuizData(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }
}