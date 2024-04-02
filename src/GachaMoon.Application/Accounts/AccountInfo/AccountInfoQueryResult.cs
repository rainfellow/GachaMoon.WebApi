using GachaMoon.Domain.Accounts;

namespace GachaMoon.Application.Accounts.AccountInfo;

public record AccountInfoQueryResult
{
    public string AccountName { get; set; } = default!;
    public AccountType AccountType { get; set; } = default!;
    public int PremiumCurrencyAmount { get; set; }
    public int WildcardSkillItemCount { get; set; }
    public int StandardBannerRollsAmount { get; set; }
}
