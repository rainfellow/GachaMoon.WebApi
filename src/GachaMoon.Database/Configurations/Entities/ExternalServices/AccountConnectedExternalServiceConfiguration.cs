using GachaMoon.Database.Extensions;
using GachaMoon.Domain.ExternalServices;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.ExternalServices;

public class AccountConnectedExternalServiceConfiguration : ConfigurationBase<AccountConnectedExternalService>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountConnectedExternalService> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.ExternalServiceProvider).IsRequired();
        _ = builder.Property(x => x.ExternalServiceType).IsRequired();
        _ = builder.Property(x => x.ExternalServiceUserId).IsRequired();
        _ = builder.Property(x => x.UserAnimeList).HasJsonConversion().IsRequired();

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);

        _ = builder.HasIndex(x => new { x.AccountId, x.ExternalServiceType }).WhereNotDeleted().IsUnique();
    }
}