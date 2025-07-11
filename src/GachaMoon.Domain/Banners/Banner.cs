// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Banners;

public class Banner : SoftDeleteEntityBase<long>
{
    public string Title { get; set; } = default!;
    public BannerType Type { get; set; } = BannerType.None;
    public DateTime? BannerExpiryDate { get; set; }

    public virtual ICollection<BannerCharacter> BannerCharacters { get; set; } = new HashSet<BannerCharacter>();
}