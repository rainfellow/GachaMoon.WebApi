using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.AccountInfo;

public class AccountInfoQuery(long accountId) : IRequest<AccountInfoQueryResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
}
