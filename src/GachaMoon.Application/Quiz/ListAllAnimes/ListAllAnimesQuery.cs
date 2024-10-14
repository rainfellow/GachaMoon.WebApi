using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Quiz.ListAllAnimes;

public class ListAllAnimesQuery(long accountId) : IRequest<ListAllAnimesQueryResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
}
