using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Users;


public class InternalUserConfiguration : ConfigurationBase<InternalUser, long>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<InternalUser> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountId)
            .IsRequired();

        _ = builder.HasIndex(x => x.AccountId)
            .WhereNotDeleted()
            .IsUnique();

        _ = builder.Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired();

        _ = builder.Property(x => x.Password)
            .HasMaxLength(80)
            .IsRequired();

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);
    }
}
