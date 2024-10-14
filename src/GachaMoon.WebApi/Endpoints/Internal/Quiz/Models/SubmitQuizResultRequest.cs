using GachaMoon.Application.Internal.SubmitQuizResult;
using GachaMoon.Domain.Quiz;

namespace GachaMoon.WebApi.Endpoints.Internal.Quiz.Models;

public record SubmitQuizResultRequest(string GameTitle, GameType GameType, GameRecap GameRecap)
{
    public SubmitQuizResultCommand ToCommand()
    {
        return new SubmitQuizResultCommand(GameTitle, GameRecap, GameType);
    }
}
