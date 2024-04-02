using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Users;

public class ExternalUserConfiguration : ConfigurationBase<ExternalUser>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<ExternalUser> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountId)
            .IsRequired();

        _ = builder.Property(x => x.Identifier)
            .HasMaxLength(255)
            .IsRequired();

        _ = builder.Property(x => x.UserType)
            .IsRequired();

        _ = builder.HasIndex(u => new { u.UserType, u.AccountId })
            .WhereNotDeleted()
            .IsUnique();

        _ = builder.HasIndex(u => new { u.UserType, u.Identifier })
            .WhereNotDeleted()
            .IsUnique();

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}


