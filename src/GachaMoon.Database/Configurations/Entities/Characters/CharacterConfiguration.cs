using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;
public class CharacterConfiguration : ConfigurationBase<Character>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<Character> builder)
    {
        base.ApplyConfiguration(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.CharacterType)
            .IsRequired();

        builder.Property(x => x.Rarity)
            .IsRequired();
    }
}