// src/GachaMoon.Database/Configurations/Entities/Characters/CharacterBaseStatsConfiguration.cs
using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;

public class CharacterBaseStatsConfiguration : ConfigurationBase<CharacterBaseStats>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<CharacterBaseStats> builder)
    {
        base.ApplyConfiguration(builder);
        builder.Property(x => x.CharacterId).IsRequired();
        builder.Property(x => x.CharacterLevel).IsRequired();
        builder.Property(x => x.Attack).IsRequired();
        builder.Property(x => x.Defence).IsRequired();
        builder.Property(x => x.Health).IsRequired();

        builder.HasIndex(x => new { x.CharacterId, x.CharacterLevel }).WhereNotDeleted().IsUnique();

        builder.HasOne(x => x.Character)
            .WithMany()
            .HasForeignKey(x => x.CharacterId);
    }
}