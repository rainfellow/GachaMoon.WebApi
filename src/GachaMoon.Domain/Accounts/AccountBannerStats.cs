using GachaMoon.Domain.Banners;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Accounts;
public class AccountBannerStats : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public virtual Account Account { get; set; } = default!;

    public BannerType BannerType { get; set; }
    public int TotalRolls { get; set; }
    public int RollsToLegendary { get; set; }
    public int RollsToEpic { get; set; }
    public int TotalEpicRolls { get; set; }
    public int TotalLegendaryRolls { get; set; }
}