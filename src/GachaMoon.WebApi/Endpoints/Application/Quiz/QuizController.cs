using GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;
using GachaMoon.Application.Quiz.GenerateScreenshotQuiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.Quiz;

public class QuizController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost("screenshots/generate")]
    public async Task<ActionResult<GenerateScreenshotQuizCommandResult>> GenerateQuestion(
        CancellationToken cancellationToken)
    {
        var command = new GenerateScreenshotQuizCommand(GetAccountId(), false);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("screenshots/check")]
    public async Task<ActionResult<CheckScreenshotQuizAnswerQueryResult>> GenerateQuestion(
        [FromQuery] string question,
        [FromQuery] string answer,
        CancellationToken cancellationToken)
    {
        var command = new CheckScreenshotQuizAnswerQuery(GetAccountId(), question, answer);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }
}
