using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;

public class CharacterAbilityConfiguration : ConfigurationBase<CharacterAbility>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<CharacterAbility> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        _ = builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
        _ = builder.Property(x => x.AbilityType).IsRequired();
        _ = builder.Property(x => x.AbilityTarget).IsRequired();
        _ = builder.Property(x => x.AbilityRange).IsRequired();

        _ = builder.HasData([
            new() {
                Id = 1,
                Name = "Placeholder: basic attack",
                Description = "Эта атака является затычкой, и не должна появляться у персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Basic,
                AbilityRange = AbilityRange.SingleTarget
            },
            new() {
                Id = 2,
                Name = "Placeholder: special attack",
                Description = "Эта атака является затычкой, и не должна появляться у персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Skill,
                AbilityRange = AbilityRange.SingleTarget
            },
            new() {
                Id = 3,
                Name = "Placeholder: ultimate attack",
                Description = "Эта атака является затычкой, и не должна появляться у персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Ultimate,
                AbilityRange = AbilityRange.SmallArea
            },
            new() {
                Id = 4,
                Name = "Placeholder: passive skill",
                Description = "Это умение является затычкой, и не должно появляться у персонажа.",
                AbilityTarget = AbilityTarget.None,
                AbilityType = AbilityType.Passive,
                AbilityRange = AbilityRange.None
            },
            new() {
                Id = 5,
                Name = "Бросок сюрикена",
                Description = "Бросает сюрикен в указанного противника, нанося урон в зависимости от силы атаки персонажа.",
                AbilityTarget = AbilityTarget.Enemy,
                AbilityType = AbilityType.Basic,
                AbilityRange = AbilityRange.SingleTarget
            },
        ]);
    }
}