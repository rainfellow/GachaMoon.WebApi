using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;

public class CharacterAbilityConfiguration : ConfigurationBase<CharacterAbility>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<CharacterAbility> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.AbilityType).IsRequired();
        builder.Property(x => x.AbilityTarget).IsRequired();
        builder.Property(x => x.AbilityRange).IsRequired();

        builder.HasData(new CharacterAbility[] {
            new CharacterAbility {
                Id = 1,
                Name = "Placeholder: basic attack",
                Description = "Эта атака является затычкой, и не должна появляться у персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Basic,
                AbilityRange = AbilityRange.SingleTarget
            },
            new CharacterAbility {
                Id = 2,
                Name = "Placeholder: special attack",
                Description = "Эта атака является затычкой, и не должна появляться у персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Skill,
                AbilityRange = AbilityRange.SingleTarget
            },
            new CharacterAbility {
                Id = 3,
                Name = "Placeholder: ultimate attack",
                Description = "Эта атака является затычкой, и не должна появляться у персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Ultimate,
                AbilityRange = AbilityRange.SmallArea
            },
            new CharacterAbility {
                Id = 4,
                Name = "Placeholder: passive skill",
                Description = "Это умение является затычкой, и не должно появляться у персонажа.",
                AbilityTarget = AbilityTarget.None,
                AbilityType = AbilityType.Passive,
                AbilityRange = AbilityRange.None
            },
            new CharacterAbility {
                Id = 5,
                Name = "Бросок сюрикена",
                Description = "Бросает сюрикен в указанного противника, нанося урон в зависимости от силы атаки персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Basic,
                AbilityRange = AbilityRange.SingleTarget
            },
        });
    }
}