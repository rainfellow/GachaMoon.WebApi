using GachaMoon.Domain.Characters;

namespace GachaMoon.Application.Characters.Common;
public record CharacterAbilityData
{
    public long AbilityId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public AbilityType AbilityType { get; set; }
    public AbilityTarget AbilityTarget { get; set; }
}