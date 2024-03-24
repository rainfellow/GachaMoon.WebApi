using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;

public class AccountCharacterAbilityConfiguration : ConfigurationBase<AccountCharacterAbility>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountCharacterAbility> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.AccountCharacterId).IsRequired();
        builder.Property(x => x.CharacterAbilityId).IsRequired();
        builder.Property(x => x.AbilityType).IsRequired();

        // Add foreign key relationship to AccountCharacter using AccountCharacterId
        builder.HasOne(x => x.AccountCharacter)
            .WithMany()
            .HasForeignKey(x => x.AccountCharacterId);

        // Add foreign key relationship to CharacterAbility using CharacterAbilityId
        builder.HasOne(x => x.CharacterAbility)
            .WithMany()
            .HasForeignKey(x => x.CharacterAbilityId);

        // Add index for AbilityType and AccountCharacter
        builder.HasIndex(x => new { x.AbilityType, x.AccountCharacterId }).WhereNotDeleted().IsUnique();
    }
}