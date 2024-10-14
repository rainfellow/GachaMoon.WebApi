using GachaMoon.Application.Quiz.ListAllAnimes;
using GachaMoon.WebApi.Endpoints.Application.Quiz.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.Quiz;

public class QuizController(ISender sender) : ApiControllerBase(sender)
{
    // [HttpPost("screenshots/generate")]
    // public async Task<ActionResult<GenerateScreenshotQuizCommandResult>> GenerateQuestion(
    //     [FromQuery] bool useList = false,
    //     CancellationToken cancellationToken = default)
    // {
    //     var command = new GenerateScreenshotQuizCommand(GetAccountId(), useList);
    //     var result = await Sender.Send(command, cancellationToken);
    //     return Ok(result);
    // }

    // [HttpGet("screenshots/check")]
    // public async Task<ActionResult<CheckScreenshotQuizAnswerQueryResult>> CheckAnswer(
    //     [FromQuery] string question,
    //     [FromQuery] string answer,
    //     CancellationToken cancellationToken)
    // {
    //     var command = new CheckScreenshotQuizAnswerQuery(GetAccountId(), question, answer);
    //     var result = await Sender.Send(command, cancellationToken);
    //     return Ok(result);
    // }
    [HttpGet("animes/all")]
    public async Task<ActionResult<ListAllAnimesQueryResult>> ListAllAnimes(
        CancellationToken cancellationToken)
    {
        var command = new ListAllAnimesQuery(GetAccountId());
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }
    [HttpPost("feedback/submit")]
    public async Task<IActionResult> SubmitGameFeedback(
        [FromBody] SubmitQuizFeedbackRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(GetAccountId());
        await Sender.Send(command, cancellationToken);
        return Ok();
    }
}
