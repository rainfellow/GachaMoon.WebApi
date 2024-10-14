using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Animes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Animes;

public class AnimeAliasConfiguration : ConfigurationBase<AnimeAlias>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AnimeAlias> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.Alias).IsRequired();
        _ = builder.Property(x => x.AnimeId).IsRequired();
        _ = builder.Property(x => x.Language).IsRequired().HasDefaultValue("ERR").HasMaxLength(3);

        _ = builder.HasOne(x => x.Anime)
            .WithMany(x => x.AnimeAliases)
            .HasForeignKey(x => x.AnimeId);

        _ = builder.HasIndex(x => x.Alias).WhereNotDeleted().IsUnique();
    }
}