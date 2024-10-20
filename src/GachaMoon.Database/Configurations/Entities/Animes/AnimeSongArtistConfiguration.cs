using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeSongArtistConfiguration : ConfigurationBase<AnimeSongArtist>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AnimeSongArtist> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.ArtistId).IsRequired();
        _ = builder.HasKey(x => x.ArtistId);
        _ = builder.Property(x => x.ArtistName).IsRequired();
        _ = builder.Property(x => x.ArtistType).IsRequired().HasDefaultValue(AnimeSongArtistType.None);
        _ = builder
                .HasMany(x => x.AnimeSongs)
                .WithMany(x => x.AnimeSongArtists);
    }
}