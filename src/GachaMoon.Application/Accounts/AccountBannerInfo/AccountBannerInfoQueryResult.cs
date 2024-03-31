using GachaMoon.Application.Accounts.Common;

namespace GachaMoon.Application.Accounts.AccountBannerInfo;

public record AccountBannerInfoQueryResult
{
    public string AccountName { get; set; } = default!;
    public ICollection<AccountBannerData> AccountBannerData { get; init; } = default!;
}
