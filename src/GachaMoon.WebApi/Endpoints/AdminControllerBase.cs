using GachaMoon.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GachaMoon.Utilities.Constants;

namespace GachaMoon.WebApi.Endpoints;

[Authorize(Policy = ApiPolicies.AdminOnlyPolicy)]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/admin/[controller]")]
public abstract class AdminControllerBase : ApiControllerBase
{
    protected AdminControllerBase(ISender sender) : base(sender)
    {

    }
}
