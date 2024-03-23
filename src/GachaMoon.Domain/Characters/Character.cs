using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Characters;

public class Character : SoftDeleteEntityBase<long>
{
    public string Name { get; set; } = default!;
    public CharacterRarity Rarity { get; set; } = default!;
    public CharacterType CharacterType { get; set; }
}
