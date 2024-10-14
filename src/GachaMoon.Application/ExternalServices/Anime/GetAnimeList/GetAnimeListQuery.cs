using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.ExternalServices.Anime.GetAnimeList;

public class GetAnimeListQuery(long accountId) : IRequest<GetAnimeListQueryResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
}
