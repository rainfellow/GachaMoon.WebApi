using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.AccountBannerInfo;

public class AccountBannerInfoQuery : IRequest<AccountBannerInfoQueryResult>, IAccountRequest
{
    public long AccountId { get; init; }

    public AccountBannerInfoQuery(long accountId)
    {
        AccountId = accountId;

    }
}
