// src/GachaMoon.Database/Configurations/Entities/Banners/BannerConfiguration.cs
using GachaMoon.Domain.Banners;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Banners;

public class BannerConfiguration : ConfigurationBase<Banner>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<Banner> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.Title).IsRequired();
        _ = builder.Property(x => x.Type).IsRequired();
        _ = builder.Property(x => x.BannerExpiryDate).IsRequired(false);
    }
}