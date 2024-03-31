using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.AccountInfo;

public class AccountInfoQuery : IRequest<AccountInfoQueryResult>, IAccountRequest
{
    public long AccountId { get; init; }

    public AccountInfoQuery(long accountId)
    {
        AccountId = accountId;
    }
}
