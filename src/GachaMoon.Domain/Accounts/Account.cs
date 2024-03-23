using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Accounts;

public class Account : SoftDeleteEntityBase<long>
{
    public string AccountName { get; set; } = default!;
    public AccountType AccountType { get; set; } = default!;
}
