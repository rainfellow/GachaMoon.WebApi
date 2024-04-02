using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.AccountBannerInfo;

public class AccountBannerInfoQuery(long accountId) : IRequest<AccountBannerInfoQueryResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
}
