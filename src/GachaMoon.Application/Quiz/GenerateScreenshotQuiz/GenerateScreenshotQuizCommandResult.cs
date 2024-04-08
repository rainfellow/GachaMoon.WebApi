namespace GachaMoon.Application.Quiz.GenerateScreenshotQuiz;

public class GenerateScreenshotQuizCommandResult(string question)
{
    public string Question { get; init; } = question;
}