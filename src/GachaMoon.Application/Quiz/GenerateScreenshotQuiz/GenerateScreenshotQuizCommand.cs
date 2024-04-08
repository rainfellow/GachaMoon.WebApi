using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Quiz.GenerateScreenshotQuiz;

public class GenerateScreenshotQuizCommand(long accountId, bool useConnectedAccount) : IRequest<GenerateScreenshotQuizCommandResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public bool UseConnectedAccount { get; init; } = useConnectedAccount;
}
