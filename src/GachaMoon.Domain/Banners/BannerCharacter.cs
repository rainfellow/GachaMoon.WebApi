// src/GachaMoon.Domain/Banners/BannerCharacter.cs
using GachaMoon.Domain.Base;
using GachaMoon.Domain.Characters;

namespace GachaMoon.Domain.Banners;
public class BannerCharacter : SoftDeleteEntityBase<long>
{
    public long BannerId { get; set; }
    public virtual Banner Banner { get; set; } = default!;
    public long CharacterId { get; set; }
    public virtual Character Character { get; set; } = default!;
}