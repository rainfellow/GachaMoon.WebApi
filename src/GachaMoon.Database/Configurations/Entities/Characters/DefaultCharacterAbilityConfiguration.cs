using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;

public class DefaultCharacterAbilityConfiguration : ConfigurationBase<DefaultCharacterAbility>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<DefaultCharacterAbility> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.CharacterId).IsRequired();
        _ = builder.Property(x => x.CharacterAbilityId).IsRequired();
        _ = builder.Property(x => x.AbilityType).IsRequired();

        // Add foreign key relationship to Character using CharacterId
        _ = builder.HasOne(x => x.Character)
            .WithMany()
            .HasForeignKey(x => x.CharacterId);

        // Add foreign key relationship to CharacterAbility using CharacterAbilityId
        _ = builder.HasOne(x => x.CharacterAbility)
            .WithMany()
            .HasForeignKey(x => x.CharacterAbilityId);

        // Add composite index for CharacterId and AbilityType
        _ = builder.HasIndex(x => new { x.CharacterId, x.AbilityType }).WhereNotDeleted().IsUnique();

        _ = builder.HasData([
            new() {
                Id = 1,
                CharacterId = 1,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 2,
                CharacterId = 1,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 3,
                CharacterId = 1,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 4,
                CharacterId = 1,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new() {
                Id = 5,
                CharacterId = 2,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 6,
                CharacterId = 2,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 7,
                CharacterId = 2,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 8,
                CharacterId = 2,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new() {
                Id = 9,
                CharacterId = 3,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 10,
                CharacterId = 3,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 11,
                CharacterId = 3,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 12,
                CharacterId = 3,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new() {
                Id = 13,
                CharacterId = 4,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 14,
                CharacterId = 4,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 15,
                CharacterId = 4,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 16,
                CharacterId = 4,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new() {
                Id = 17,
                CharacterId = 5,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 18,
                CharacterId = 5,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 19,
                CharacterId = 5,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 20,
                CharacterId = 5,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new() {
                Id = 21,
                CharacterId = 6,
                CharacterAbilityId = 5,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 22,
                CharacterId = 6,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 23,
                CharacterId = 6,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 24,
                CharacterId = 6,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            },
            new() {
                Id = 25,
                CharacterId = 7,
                CharacterAbilityId = 1,
                AbilityType = AbilityType.Basic
            },
            new() {
                Id = 26,
                CharacterId = 7,
                CharacterAbilityId = 2,
                AbilityType = AbilityType.Skill
            },
            new() {
                Id = 27,
                CharacterId = 7,
                CharacterAbilityId = 3,
                AbilityType = AbilityType.Ultimate
            },
            new() {
                Id = 28,
                CharacterId = 7,
                CharacterAbilityId = 4,
                AbilityType = AbilityType.Passive
            }
        ]);
    }
}