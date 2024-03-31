using GachaMoon.Domain.Characters;

namespace GachaMoon.Application.Characters.Common;
public record CharacterData
{
    public long CharacterId { get; set; }
    public string Name { get; set; } = default!;
    public CharacterType Type { get; set; }
    public CharacterRarity Rarity { get; set; }
    public int BaseHealth { get; set; }
    public int BaseAttack { get; set; }
    public int BaseDefence { get; set; }
    public ICollection<CharacterDefaultAbilityData> Abilities { get; init; } = default!;
}