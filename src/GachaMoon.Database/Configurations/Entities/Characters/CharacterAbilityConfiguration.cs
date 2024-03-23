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
    }
}