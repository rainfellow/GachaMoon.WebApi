using GachaMoon.Application.User.External.DiscordLogin;
using GachaMoon.Application.User.External.DiscordSignup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Internal.Discord;

public class DiscordController(ISender sender) : InternalApiControllerBase(sender)
{
    [HttpGet("auth/token")]
    public async Task<ActionResult<InternalDiscordLoginCommandResult>> GetUserToken([FromQuery] string discordIdentifier, CancellationToken cancellationToken)
    {
        var query = new InternalDiscordLoginCommand(discordIdentifier);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("auth/signup")]
    public async Task<ActionResult<DiscordSignUpCommandResult>> RegisterUser([FromQuery] string discordIdentifier, [FromQuery] string accountName, CancellationToken cancellationToken)
    {
        var query = new DiscordSignUpCommand(discordIdentifier, accountName);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}