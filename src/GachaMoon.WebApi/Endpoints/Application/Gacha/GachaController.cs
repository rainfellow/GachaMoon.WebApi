using GachaMoon.Application.Gacha.Roll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.Gacha;

public class GachaController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost("roll")]
    public async Task<ActionResult<RollGachaCommandResult>> RollGacha(
        [FromQuery] long bannerId,
        [FromQuery] int rollCount,
        CancellationToken cancellationToken)
    {
        var command = new RollGachaCommand(GetAccountId(), bannerId, rollCount);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }
}
