using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;
public class PremiumInventoryConfiguration : ConfigurationBase<PremiumInventory>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<PremiumInventory> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.PremiumCurrencyAmount).IsRequired();
        _ = builder.Property(x => x.WildcardSkillItemCount).IsRequired();
        _ = builder.Property(x => x.StandardBannerRollsAmount).IsRequired().HasDefaultValue(0);

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}