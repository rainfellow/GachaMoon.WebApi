// src/GachaMoon.Domain/PromoCode.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Promocodes;
public class PromocodeEffect : SoftDeleteEntityBase<long>
{
    public long PromoCodeId { get; set; }
    public virtual Promocode Promocode { get; set; } = default!;

    public PromocodeEffectType EffectType { get; set; } = default!;
    public int EffectAmount { get; set; } = default!;
}