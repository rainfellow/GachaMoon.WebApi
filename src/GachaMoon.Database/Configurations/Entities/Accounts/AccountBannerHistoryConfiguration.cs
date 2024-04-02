using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;
public class AccountBannerHistoryConfiguration : ConfigurationBase<AccountBannerHistory>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountBannerHistory> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.BannerId).IsRequired();
        _ = builder.Property(x => x.Result).IsRequired();

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);

        _ = builder.HasOne(x => x.Banner)
            .WithMany() // Assuming there is a navigation property for Banner
            .HasForeignKey(x => x.BannerId);
    }
}