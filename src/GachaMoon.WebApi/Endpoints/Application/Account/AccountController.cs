using GachaMoon.Application.Accounts.AccountBannerInfo;
using GachaMoon.Application.Accounts.AccountInfo;
using GachaMoon.Application.Accounts.ListAccountCharacters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.Account;

public class AccountController : ApiControllerBase
{
    public AccountController(ISender sender) : base(sender)
    {
    }

    [HttpGet("me")]
    public async Task<ActionResult<AccountInfoQueryResult>> GetAccountInfo(
        CancellationToken cancellationToken)
    {
        var query = new AccountInfoQuery(GetAccountId());
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("mybannerstats")]
    public async Task<ActionResult<AccountBannerInfoQueryResult>> GetAccountBannerInfo(
        CancellationToken cancellationToken)
    {
        var query = new AccountBannerInfoQuery(GetAccountId());
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("mycharacters")]
    public async Task<ActionResult<ListAccountCharactersQueryResult>> ListAccountCharacters(
        CancellationToken cancellationToken)
    {
        var query = new ListAccountCharactersQuery(GetAccountId());
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
