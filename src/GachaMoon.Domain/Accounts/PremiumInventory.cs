using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Accounts;

public class PremiumInventory : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public int PremiumCurrency { get; set; }

    public virtual Account Account { get; set; } = default!;
}