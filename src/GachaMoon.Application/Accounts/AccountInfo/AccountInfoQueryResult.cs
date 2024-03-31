using GachaMoon.Domain.Accounts;

namespace GachaMoon.Application.Accounts.AccountInfo;

public record AccountInfoQueryResult
{
    public string AccountName { get; set; } = default!;
    public AccountType AccountType { get; set; } = default!;
}
