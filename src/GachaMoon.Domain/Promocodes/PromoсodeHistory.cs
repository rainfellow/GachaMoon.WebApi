// src/GachaMoon.Domain/PromoCodeHistory.cs
using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Promocodes;
public class PromocodeHistory : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public virtual Account Account { get; set; } = default!;
    public long PromoCodeId { get; set; }
    public virtual Promocode Promocode { get; set; } = default!;
};
