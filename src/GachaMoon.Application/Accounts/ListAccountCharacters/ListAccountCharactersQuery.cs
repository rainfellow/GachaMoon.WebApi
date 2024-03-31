using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.ListAccountCharacters;

public class ListAccountCharactersQuery : IRequest<ListAccountCharactersQueryResult>, IAccountRequest
{
    public long AccountId { get; init; }

    public ListAccountCharactersQuery(long accountId)
    {
        AccountId = accountId;

    }
}
