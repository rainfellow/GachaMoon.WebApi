using GachaMoon.Domain.Characters;

namespace GachaMoon.Application.Accounts.Common;
public record AccountCharacterAbilityData
{
    public long CharacterAbilityId { get; set; }
    public AbilityType AbilityType { get; set; }
}