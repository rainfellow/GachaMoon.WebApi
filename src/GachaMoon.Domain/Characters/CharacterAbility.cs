using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Characters;

public class CharacterAbility : SoftDeleteEntityBase<long>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public AbilityType AbilityType { get; set; }
    public AbilityTarget AbilityTarget { get; set; }
    public AbilityRange AbilityRange { get; set; }
}