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
    }
}