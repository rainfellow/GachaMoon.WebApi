using GachaMoon.Application.Promocodes.Redeem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.Promocode;

public class PromocodeController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost("redeem")]
    public async Task<ActionResult<RedeemPromocodeCommandResult>> RedeemPromocode(
        [FromQuery] string promocode,
        CancellationToken cancellationToken)
    {
        var command = new RedeemPromocodeCommand(GetAccountId(), promocode);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }
}
