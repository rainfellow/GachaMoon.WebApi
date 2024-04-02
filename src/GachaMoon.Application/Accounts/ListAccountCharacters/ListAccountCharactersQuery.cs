using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.ListAccountCharacters;

public class ListAccountCharactersQuery(long accountId) : IRequest<ListAccountCharactersQueryResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
}
