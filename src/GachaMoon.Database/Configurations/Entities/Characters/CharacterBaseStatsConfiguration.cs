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

        builder.HasData(new CharacterBaseStats[] {
            new CharacterBaseStats {
                Id = 1,
                CharacterId = 1,
                CharacterLevel = 1,
                Attack = 150,
                Defence = 80,
                Health = 100
            },
            new CharacterBaseStats {
                Id = 2,
                CharacterId = 2,
                CharacterLevel = 1,
                Attack = 100,
                Defence = 140,
                Health = 100
            },
            new CharacterBaseStats {
                Id = 3,
                CharacterId = 3,
                CharacterLevel = 1,
                Attack = 160,
                Defence = 70,
                Health = 100
            },
            new CharacterBaseStats {
                Id = 4,
                CharacterId = 4,
                CharacterLevel = 1,
                Attack = 60,
                Defence = 100,
                Health = 140
            },
            new CharacterBaseStats {
                Id = 5,
                CharacterId = 5,
                CharacterLevel = 1,
                Attack = 140,
                Defence = 50,
                Health = 90
            },
            new CharacterBaseStats {
                Id = 6,
                CharacterId = 6,
                CharacterLevel = 1,
                Attack = 120,
                Defence = 70,
                Health = 100
            },
            new CharacterBaseStats {
                Id = 7,
                CharacterId = 7,
                CharacterLevel = 1,
                Attack = 130,
                Defence = 90,
                Health = 70
            },
        });
    }
}