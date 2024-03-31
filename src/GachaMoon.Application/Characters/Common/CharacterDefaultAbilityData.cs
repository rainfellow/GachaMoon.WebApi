using GachaMoon.Domain.Characters;

namespace GachaMoon.Application.Characters.Common;
public record CharacterDefaultAbilityData
{
    public long CharacterAbilityId { get; set; }
    public AbilityType AbilityType { get; set; }
}