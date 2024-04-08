using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;

public class CheckScreenshotQuizAnswerQuery(long accountId, string question, string answer) : IRequest<CheckScreenshotQuizAnswerQueryResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public string Question { get; init; } = question;
    public string Answer { get; init; } = answer;
}
