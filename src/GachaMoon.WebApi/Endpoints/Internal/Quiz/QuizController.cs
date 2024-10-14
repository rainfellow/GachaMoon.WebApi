using GachaMoon.Application.Internal.GetQuizQuestions;
using GachaMoon.WebApi.Endpoints.Internal.Quiz.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Internal.Quiz;

public class QuizController(ISender sender) : InternalApiControllerBase(sender)
{
    [HttpPost("generate")]
    public async Task<ActionResult<GetQuizQuestionsQueryResult>> GenerateQuiz(
        [FromBody] GetQuizQuestionsRequest request,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("submitresult")]
    public async Task<IActionResult> SubmitQuizResult(
        [FromBody] SubmitQuizResultRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        await Sender.Send(command, cancellationToken);
        return Ok();
    }
}