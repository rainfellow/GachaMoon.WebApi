// src/GachaMoon.Database/Configurations/Entities/Banners/BannerConfiguration.cs
using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeConfiguration : ConfigurationBase<Anime>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<Anime> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.Title).IsRequired();
        _ = builder.Property(x => x.AnimeBaseId).IsRequired();
        _ = builder.Property(x => x.AnilistId).IsRequired();
        _ = builder.Property(x => x.ImageSiteTitle).IsRequired();

        _ = builder.Property(x => x.EpisodeCount).IsRequired().HasDefaultValue(0);
        _ = builder.Property(x => x.AnimeType).IsRequired().HasDefaultValue("ERR").HasMaxLength(10);
        _ = builder.Property(x => x.AgeRating).IsRequired().HasDefaultValue("ERR").HasMaxLength(8);
        _ = builder.Property(x => x.MeanScore).IsRequired().HasDefaultValue(0.0);
        _ = builder.Property(x => x.StartDate).IsRequired().HasDefaultValue("ERR").HasMaxLength(30);

        _ = builder.HasIndex(x => x.AnimeBaseId).WhereNotDeleted().IsUnique();
        _ = builder.HasIndex(x => x.AnilistId).WhereNotDeleted().IsUnique();
    }
}