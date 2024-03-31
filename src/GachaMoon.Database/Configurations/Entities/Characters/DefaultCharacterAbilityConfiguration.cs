using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;

public class DefaultCharacterAbilityConfiguration : ConfigurationBase<DefaultCharacterAbility>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<DefaultCharacterAbility> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.CharacterId).IsRequired();
        builder.Property(x => x.CharacterAbilityId).IsRequired();
        builder.Property(x => x.AbilityType).IsRequired();

        // Add foreign key relationship to Character using CharacterId
        builder.HasOne(x => x.Character)
            .WithMany()
            .HasForeignKey(x => x.CharacterId);

        // Add foreign key relationship to CharacterAbility using CharacterAbilityId
        builder.HasOne(x => x.CharacterAbility)
            .WithMany()
            .HasForeignKey(x => x.CharacterAbilityId);

        // Add composite index for CharacterId and AbilityType
        builder.HasIndex(x => new { x.CharacterId, x.AbilityType }).WhereNotDeleted().IsUnique();

        builder.HasData(new DefaultCharacterAbility[] {
            new DefaultCharacterAbility {
                Id = 1,
                CharacterId = 1,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 2,
                CharacterId = 1,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 3,
                CharacterId = 1,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 4,
                CharacterId = 1,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new DefaultCharacterAbility {
                Id = 5,
                CharacterId = 2,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 6,
                CharacterId = 2,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 7,
                CharacterId = 2,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 8,
                CharacterId = 2,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new DefaultCharacterAbility {
                Id = 9,
                CharacterId = 3,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 10,
                CharacterId = 3,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 11,
                CharacterId = 3,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 12,
                CharacterId = 3,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new DefaultCharacterAbility {
                Id = 13,
                CharacterId = 4,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 14,
                CharacterId = 4,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 15,
                CharacterId = 4,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 16,
                CharacterId = 4,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new DefaultCharacterAbility {
                Id = 17,
                CharacterId = 5,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 18,
                CharacterId = 5,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 19,
                CharacterId = 5,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 20,
                CharacterId = 5,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new DefaultCharacterAbility {
                Id = 21,
                CharacterId = 6,
                CharacterAbilityId = 5,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 22,
                CharacterId = 6,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 23,
                CharacterId = 6,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 24,
                CharacterId = 6,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new DefaultCharacterAbility {
                Id = 25,
                CharacterId = 7,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new DefaultCharacterAbility {
                Id = 26,
                CharacterId = 7,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new DefaultCharacterAbility {
                Id = 27,
                CharacterId = 7,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new DefaultCharacterAbility {
                Id = 28,
                CharacterId = 7,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            }
        });
    }
}