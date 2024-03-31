using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Users;

public class ExternalUserConfiguration : ConfigurationBase<ExternalUser>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<ExternalUser> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.AccountId)
            .IsRequired();

        builder.Property(x => x.Identifier)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.UserType)
            .IsRequired();

        builder.HasIndex(u => new { u.UserType, u.AccountId })
            .WhereNotDeleted()
            .IsUnique();

        builder.HasIndex(u => new { u.UserType, u.Identifier })
            .WhereNotDeleted()
            .IsUnique();

        builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}


