using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;

public class AccountCharacterConfiguration : ConfigurationBase<AccountCharacter>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountCharacter> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.CharacterId).IsRequired();
        builder.Property(x => x.RepeatCount).IsRequired();
        builder.Property(x => x.TotalSkillPoints).IsRequired();
        builder.Property(x => x.FreeSkillPoints).IsRequired();
        builder.Property(x => x.SkillTree).IsRequired();
        builder.Property(x => x.CharacterLevel).IsRequired();
        builder.Property(x => x.CharacterExperience).IsRequired();

        builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);

        builder.HasOne(x => x.Character)
            .WithMany() // Assuming there is a navigation property for Character
            .HasForeignKey(x => x.CharacterId);

        builder.HasIndex(x => new { x.AccountId, x.CharacterId }).WhereNotDeleted().IsUnique();
    }
}