// src/GachaMoon.Database/Configurations/Entities/Banners/BannerConfiguration.cs
using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeImageConfiguration : ConfigurationBase<AnimeImage>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AnimeImage> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.Url).IsRequired();
        _ = builder.Property(x => x.VoteCount).IsRequired();
        _ = builder.Property(x => x.VoteSum).IsRequired();
        _ = builder.Property(x => x.BadVoteCount).IsRequired();
        _ = builder.Property(x => x.AnimeEpisodeId).IsRequired();

        _ = builder.HasOne(x => x.AnimeEpisode)
            .WithMany(x => x.AnimeImages)
            .HasForeignKey(x => x.AnimeEpisodeId);
    }
}