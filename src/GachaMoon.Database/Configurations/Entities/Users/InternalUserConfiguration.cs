using GachaMoon.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Users;


public class InternalUserConfiguration : ConfigurationBase<InternalUser, long>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<InternalUser> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.AccountId)
            .IsRequired();

        builder.HasIndex(x => x.AccountId)
            .IsUnique();

        builder.Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(80)
            .IsRequired();

        builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}
