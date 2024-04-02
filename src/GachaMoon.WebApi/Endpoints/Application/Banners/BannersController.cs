using GachaMoon.Application.Banners.List;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace GachaMoon.WebApi.Endpoints.Application.Banners;

public class BannersController(ISender sender) : ApiControllerBase(sender)
{
    [AllowAnonymous]
    [OutputCache(Duration = 300)]
    [HttpGet("list")]
    public async Task<ActionResult<ListBannersQueryResult>> ListBanners(
        CancellationToken cancellationToken)
    {
        var query = new ListBannersQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
