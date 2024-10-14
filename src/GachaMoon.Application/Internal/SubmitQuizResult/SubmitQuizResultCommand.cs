using GachaMoon.Domain.Quiz;

namespace GachaMoon.Application.Internal.SubmitQuizResult;

public class SubmitQuizResultCommand(string gameTitle, GameRecap gameRecap, GameType gameType) : IRequest
{
    public string GameTitle { get; init; } = gameTitle;
    public GameRecap GameRecap { get; init; } = gameRecap;
    public GameType GameType { get; init; } = gameType;
}
