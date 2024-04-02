using GachaMoon.Application.Characters.List;
using GachaMoon.Application.Characters.ListAbilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace GachaMoon.WebApi.Endpoints.Internal.GameObjects;

public class GameObjectsController(ISender sender) : InternalApiControllerBase(sender)
{
    [HttpGet("characters")]
    [OutputCache(Duration = 180)]
    public async Task<ActionResult<ListCharactersQueryResult>> GetCharacters(CancellationToken cancellationToken)
    {
        var query = new ListCharactersQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("abilities")]
    [OutputCache(Duration = 180)]
    public async Task<ActionResult<ListCharacterAbilitiesQueryResult>> GetAbilities(CancellationToken cancellationToken)
    {
        var query = new ListCharacterAbilitiesQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}