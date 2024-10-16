using GachaMoon.Application.Internal.GetAccountInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Internal.Account;

public class AccountController(ISender sender) : InternalApiControllerBase(sender)
{
    [HttpGet("info")]
    public async Task<ActionResult<InternalAccountInfoQueryResult>> GetAccountInfo(
        [FromQuery] long accountId,
        CancellationToken cancellationToken)
    {
        var query = new InternalAccountInfoQuery(accountId);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
