using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;
public class AccountBannerHistoryConfiguration : ConfigurationBase<AccountBannerHistory>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountBannerHistory> builder)
    {
        base.ApplyConfiguration(builder);
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.BannerId).IsRequired();
        builder.Property(x => x.Result).IsRequired();

        builder.HasIndex(x => new { x.AccountId, x.BannerId }).WhereNotDeleted().IsUnique();

        builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);

        builder.HasOne(x => x.Banner)
            .WithMany() // Assuming there is a navigation property for Banner
            .HasForeignKey(x => x.BannerId);
    }
}