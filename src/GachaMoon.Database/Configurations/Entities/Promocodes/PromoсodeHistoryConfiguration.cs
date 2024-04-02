// src/GachaMoon.Database/Configurations/PromoCodeHistoryConfiguration.cs
using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Promocodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Promocodes;
public class PromocodeHistoryConfiguration : IEntityTypeConfiguration<PromocodeHistory>
{
    public void Configure(EntityTypeBuilder<PromocodeHistory> builder)
    {
        _ = builder.HasIndex(p => new { p.AccountId, p.PromoCodeId }).WhereNotDeleted().IsUnique();
        _ = builder.HasOne(p => p.Account)
            .WithMany()
            .HasForeignKey(p => p.AccountId);
        _ = builder.HasOne(p => p.Promocode)
            .WithMany()
            .HasForeignKey(p => p.PromoCodeId);
    }
}