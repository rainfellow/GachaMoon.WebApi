using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Characters;

public class DefaultCharacterAbility : SoftDeleteEntityBase<long>
{
    public long CharacterId { get; set; } = default!;
    public long CharacterAbilityId { get; set; } = default!;
    public AbilityType AbilityType { get; set; } = default!;

    public virtual Character Character { get; set; } = default!;
    public virtual CharacterAbility CharacterAbility { get; set; } = default!;
}
