namespace GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;

public class CheckScreenshotQuizAnswerQueryResult(bool result)
{
    public bool Result { get; init; } = result;
}