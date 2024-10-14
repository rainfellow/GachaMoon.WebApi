using GachaMoon.WebApi.Endpoints.Application.BugReports.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.BugReports;

public class BugReportsController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost("alias/submit")]
    public async Task<IActionResult> SubmitAliasBugReport(
        [FromBody] SubmitAliasBugReportRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(GetAccountId());
        await Sender.Send(command, cancellationToken);
        return Ok();
    }
}
