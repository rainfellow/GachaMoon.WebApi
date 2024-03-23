using GachaMoon.Domain.Base;
using GachaMoon.Domain.Characters;

namespace GachaMoon.Domain.Accounts;

public class AccountCharacterAbility : SoftDeleteEntityBase<long>
{
    public long AccountCharacterId { get; set; } = default!;
    public long CharacterAbilityId { get; set; } = default!;
    public AbilityType AbilityType { get; set; } = default!;

    public virtual AccountCharacter AccountCharacter { get; set; } = default!;
    public virtual CharacterAbility CharacterAbility { get; set; } = default!;
}
