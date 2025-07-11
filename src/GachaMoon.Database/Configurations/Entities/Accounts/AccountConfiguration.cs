using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;
public class AccountConfiguration : ConfigurationBase<Account>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<Account> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountName)
            .HasMaxLength(255)
            .IsRequired();

        _ = builder.Property(x => x.AccountType)
            .IsRequired();
    }
}