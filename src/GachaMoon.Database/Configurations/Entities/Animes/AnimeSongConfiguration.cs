using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeSongConfiguration : ConfigurationBase<AnimeSong>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AnimeSong> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AnimeId).IsRequired();
        _ = builder.Property(x => x.AMQSongDifficulty).IsRequired();
        _ = builder.Property(x => x.SongName).IsRequired();
        _ = builder.Property(x => x.SongType).IsRequired().HasDefaultValue(SongType.None);

        _ = builder.HasOne(x => x.Anime).WithMany(x => x.AnimeSongs).HasForeignKey(x => x.AnimeId);
        _ = builder
                .HasMany(x => x.AnimeSongArtists)
                .WithMany(x => x.AnimeSongs);


    }
}