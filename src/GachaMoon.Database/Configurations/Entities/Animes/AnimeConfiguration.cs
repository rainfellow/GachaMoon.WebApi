// src/GachaMoon.Database/Configurations/Entities/Banners/BannerConfiguration.cs
using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeConfiguration : ConfigurationBase<Anime>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<Anime> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.Title).IsRequired();
        _ = builder.Property(x => x.AnimeBaseId).IsRequired();
        _ = builder.Property(x => x.ImageSiteTitle).IsRequired();

        _ = builder.HasIndex(x => x.AnimeBaseId).WhereNotDeleted().IsUnique();
    }
}