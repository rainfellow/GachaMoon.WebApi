using System.Globalization;
using GachaMoon.Common.Exceptions.Login;
using GachaMoon.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected ISender Sender { get; }

    protected long GetUserId()
    {
        return User.Identity?.Name == null ? throw new MissingIdentityException()
            : long.Parse(User.Identity.Name, CultureInfo.InvariantCulture);
    }

    protected ApiControllerBase(ISender sender)
    {
        Sender = sender;
    }
}
