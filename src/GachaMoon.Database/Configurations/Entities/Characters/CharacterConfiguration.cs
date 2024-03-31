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

        builder.HasData(new Character[] {
            new Character {
                Id = 1,
                CharacterType = CharacterType.Destruction,
                Name = "Ромихи",
                Rarity = CharacterRarity.Legendary
            },
            new Character {
                Id = 2,
                CharacterType = CharacterType.Protection,
                Name = "Шувидор",
                Rarity = CharacterRarity.Legendary
            },
            new Character {
                Id = 3,
                CharacterType = CharacterType.Trickery,
                Name = "Пациент 163",
                Rarity = CharacterRarity.Legendary
            },
            new Character {
                Id = 4,
                CharacterType = CharacterType.Creation,
                Name = "Чехов",
                Rarity = CharacterRarity.Epic
            },
            new Character {
                Id = 5,
                CharacterType = CharacterType.Destruction,
                Name = "Черная Мамба",
                Rarity = CharacterRarity.Epic
            },
            new Character {
                Id = 6,
                CharacterType = CharacterType.Destruction,
                Name = "Яна Цист",
                Rarity = CharacterRarity.Epic
            },
            new Character {
                Id = 7,
                CharacterType = CharacterType.Trickery,
                Name = "Кедонап",
                Rarity = CharacterRarity.Epic
            },
        });
    }
}