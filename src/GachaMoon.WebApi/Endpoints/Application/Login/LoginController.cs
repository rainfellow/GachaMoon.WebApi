using GachaMoon.WebApi.Endpoints.Application.Login.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.Login;

[AllowAnonymous]
public class LoginController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await Sender.Send(command, cancellationToken);
        return Ok(LoginResponse.FromResult(result));
    }
}
