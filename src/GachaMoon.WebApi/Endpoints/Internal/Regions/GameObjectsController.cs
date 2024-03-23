using MediatR;

namespace GachaMoon.WebApi.Endpoints.Internal.Regions;

public class CharactersController : InternalApiControllerBase
{
    public CharactersController(ISender sender) : base(sender)
    {
    }
}