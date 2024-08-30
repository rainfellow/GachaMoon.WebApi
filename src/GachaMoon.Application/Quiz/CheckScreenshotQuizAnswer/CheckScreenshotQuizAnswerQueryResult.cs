namespace GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;

public class CheckScreenshotQuizAnswerQueryResult(string correctAnswer, bool result)
{
    public string CorrectAnswer { get; init; } = correctAnswer;
    public bool Result { get; init; } = result;
}