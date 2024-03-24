// src/GachaMoon.Domain/PromoCode.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Promocodes;
public class Promocode : SoftDeleteEntityBase<long>
{
    public string Code { get; set; } = default!;
    public DateOnly ExpiryDate { get; set; }
    public int UsesLeft { get; set; }
};