using GachaMoon.Application.User.UserInfo;
using GachaMoon.WebApi.Endpoints.Application.User.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints.Application.User;

public class UserController : ApiControllerBase
{
    public UserController(ISender sender) : base(sender)
    {
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult<SignupResponse>> Signup(
        [FromBody] SignupRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await Sender.Send(command, cancellationToken);
        return Ok(SignupResponse.FromResult(result));
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserInfoResponse>> UserInfo(CancellationToken cancellationToken)
    {
        var query = new UserInfoQuery(GetUserId());
        var result = await Sender.Send(query, cancellationToken);
        return Ok(UserInfoResponse.FromResult(result));
    }

    [HttpPost("me")]
    public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequest request, CancellationToken cancellationToken)
    {
        var query = request.ToCommand(GetUserId());
        await Sender.Send(query, cancellationToken);
        return Ok();
    }

    [HttpPost("password/change")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var query = request.ToCommand(GetUserId());
        await Sender.Send(query, cancellationToken);
        return Ok();
    }
}
