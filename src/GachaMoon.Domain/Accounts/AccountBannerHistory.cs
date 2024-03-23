using GachaMoon.Domain.Banners;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Accounts;
public class AccountBannerHistory : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public virtual Account Account { get; set; } = default!;

    public long BannerId { get; set; }
    public virtual Banner Banner { get; set; } = default!;

    public string Result { get; set; } = default!;
}