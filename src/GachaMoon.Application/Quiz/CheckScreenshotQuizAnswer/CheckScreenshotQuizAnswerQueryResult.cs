namespace GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;

public class CheckScreenshotQuizAnswerQueryResult(string correctAnswer, string predictedAnswer, bool result)
{
    public string CorrectAnswer { get; init; } = correctAnswer;
    public string PredictedAnswer { get; init; } = predictedAnswer;
    public bool Result { get; init; } = result;
}