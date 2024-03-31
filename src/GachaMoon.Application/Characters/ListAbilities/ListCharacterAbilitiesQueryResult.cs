using GachaMoon.Application.Characters.Common;

namespace GachaMoon.Application.Characters.ListAbilities;

public record ListCharacterAbilitiesQueryResult(ICollection<CharacterAbilityData> Abilities)
{
}
