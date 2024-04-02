using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Accounts;

public class AccountCharacterConfiguration : ConfigurationBase<AccountCharacter>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AccountCharacter> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.CharacterId).IsRequired();
        _ = builder.Property(x => x.RepeatCount).IsRequired();
        _ = builder.Property(x => x.TotalSkillPoints).IsRequired();
        _ = builder.Property(x => x.FreeSkillPoints).IsRequired();
        _ = builder.Property(x => x.SkillTree).IsRequired();
        _ = builder.Property(x => x.CharacterLevel).IsRequired();
        _ = builder.Property(x => x.CharacterExperience).IsRequired();

        _ = builder.HasOne(x => x.Account)
            .WithMany() // Assuming there is a navigation property for Account
            .HasForeignKey(x => x.AccountId);

        _ = builder.HasOne(x => x.Character)
            .WithMany() // Assuming there is a navigation property for Character
            .HasForeignKey(x => x.CharacterId);

        _ = builder.HasIndex(x => new { x.AccountId, x.CharacterId }).WhereNotDeleted().IsUnique();
    }
}