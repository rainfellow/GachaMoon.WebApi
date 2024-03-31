using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;
public class AccountBannerStatsConfiguration : ConfigurationBase<AccountBannerStats>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountBannerStats> builder)
    {
        base.ApplyConfiguration(builder);
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.BannerType).IsRequired();
        builder.Property(x => x.TotalRolls).IsRequired();
        builder.Property(x => x.RollsToLegendary).IsRequired();
        builder.Property(x => x.RollsToEpic).IsRequired();
        builder.Property(x => x.TotalLegendaryRolls).IsRequired();
        builder.Property(x => x.TotalEpicRolls).IsRequired();

        builder.HasIndex(x => new { x.AccountId, x.BannerType }).WhereNotDeleted().IsUnique();

        builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}