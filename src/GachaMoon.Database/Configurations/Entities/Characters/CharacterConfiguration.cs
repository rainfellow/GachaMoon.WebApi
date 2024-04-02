using GachaMoon.Domain.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Characters;
public class CharacterConfiguration : ConfigurationBase<Character>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<Character> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        _ = builder.Property(x => x.CharacterType)
            .IsRequired();

        _ = builder.Property(x => x.Rarity)
            .IsRequired();

        _ = builder.HasData([
            new() {
                Id = 1,
                CharacterType = CharacterType.Destruction,
                Name = "Аккихи",
                Rarity = CharacterRarity.Legendary
            },
            new() {
                Id = 2,
                CharacterType = CharacterType.Protection,
                Name = "Шувидор",
                Rarity = CharacterRarity.Legendary
            },
            new() {
                Id = 3,
                CharacterType = CharacterType.Trickery,
                Name = "Кадзуал",
                Rarity = CharacterRarity.Legendary
            },
            new() {
                Id = 4,
                CharacterType = CharacterType.Creation,
                Name = "Чехов",
                Rarity = CharacterRarity.Epic
            },
            new() {
                Id = 5,
                CharacterType = CharacterType.Destruction,
                Name = "Черная Мамба",
                Rarity = CharacterRarity.Epic
            },
            new() {
                Id = 6,
                CharacterType = CharacterType.Destruction,
                Name = "Яна Цист",
                Rarity = CharacterRarity.Epic
            },
            new() {
                Id = 7,
                CharacterType = CharacterType.Trickery,
                Name = "Панкоед",
                Rarity = CharacterRarity.Epic
            },
        ]);
    }
}