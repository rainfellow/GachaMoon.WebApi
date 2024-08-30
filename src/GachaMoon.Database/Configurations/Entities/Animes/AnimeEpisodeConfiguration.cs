// src/GachaMoon.Database/Configurations/Entities/Banners/BannerConfiguration.cs
using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeEpisodeConfiguration : ConfigurationBase<AnimeEpisode>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AnimeEpisode> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.Title).IsRequired();
        _ = builder.Property(x => x.AnimeId).IsRequired();

        _ = builder.HasOne(x => x.Anime)
            .WithMany(x => x.AnimeEpisodes)
            .HasForeignKey(x => x.AnimeId);
    }
}