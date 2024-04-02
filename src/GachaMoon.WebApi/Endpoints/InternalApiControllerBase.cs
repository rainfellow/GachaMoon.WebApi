using GachaMoon.Common.Attributes;
using GachaMoon.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GachaMoon.WebApi.Endpoints;

[ApiKey]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("internal-api/v{version:apiVersion}/[controller]")]
public abstract class InternalApiControllerBase(ISender sender) : ControllerBase
{
    protected ISender Sender { get; } = sender;
}
