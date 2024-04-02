using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Promocodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Promocodes;
public class PromocodeEffectConfiguration : IEntityTypeConfiguration<PromocodeEffect>
{
    public void Configure(EntityTypeBuilder<PromocodeEffect> builder)
    {
        _ = builder.Property(p => p.PromoCodeId).IsRequired();
        _ = builder.Property(p => p.EffectAmount).IsRequired();
        _ = builder.Property(p => p.EffectType).IsRequired();

        _ = builder.HasIndex(p => new { p.PromoCodeId, p.EffectType }).WhereNotDeleted().IsUnique();

        _ = builder.HasOne(p => p.Promocode)
            .WithMany(x => x.PromocodeEffects)
            .HasForeignKey(p => p.PromoCodeId);

        _ = builder.HasData([
            new() {
                Id = 1,
                PromoCodeId = 1,
                EffectType = PromocodeEffectType.GivePremiumCurrency,
                EffectAmount = 1000
            },
            new() {
                Id = 2,
                PromoCodeId = 2,
                EffectType = PromocodeEffectType.GivePremiumCurrency,
                EffectAmount = 1000
            },
            new() {
                Id = 3,
                PromoCodeId = 3,
                EffectType = PromocodeEffectType.GiveStandardRolls,
                EffectAmount = 10
            }
        ]);
    }
};