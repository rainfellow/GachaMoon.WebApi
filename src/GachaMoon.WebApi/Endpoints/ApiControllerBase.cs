using System.Globalization;
using GachaMoon.Common.Exceptions.Login;
using GachaMoon.Utilities.Constants;
using GachaMoon.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase(ISender sender) : ControllerBase
{
    protected ISender Sender { get; } = sender;

    protected long GetAccountId()
    {
        return User.Identity?.Name == null ? throw new MissingIdentityException()
            : long.Parse(User.Identity.Name, CultureInfo.InvariantCulture);
    }

    protected long GetUserId()
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == UserClaims.UserIdClaim);
        return claim?.Value == null ? throw new MissingIdentityException()
            : long.Parse(claim.Value, CultureInfo.InvariantCulture);
    }

    protected bool GetUserLoginMethod()
    {
        throw new NotImplementedException();
    }
}
