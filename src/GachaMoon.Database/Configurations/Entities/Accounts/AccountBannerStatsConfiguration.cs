using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;
public class AccountBannerStatsConfiguration : ConfigurationBase<AccountBannerStats>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountBannerStats> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.BannerType).IsRequired();
        _ = builder.Property(x => x.TotalRolls).IsRequired();
        _ = builder.Property(x => x.RollsToLegendary).IsRequired();
        _ = builder.Property(x => x.RollsToEpic).IsRequired();
        _ = builder.Property(x => x.TotalLegendaryRolls).IsRequired();
        _ = builder.Property(x => x.TotalEpicRolls).IsRequired();

        _ = builder.HasIndex(x => new { x.AccountId, x.BannerType }).WhereNotDeleted().IsUnique();

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}