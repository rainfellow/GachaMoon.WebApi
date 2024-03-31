namespace GachaMoon.Application.Accounts.Common;
public record AccountCharacterData
{
    public long CharacterId { get; set; }
    public int CharacterLevel { get; set; }
    public int CharacterExperience { get; set; }
    public int RepeatCount { get; set; }
    public ICollection<AccountCharacterAbilityData> Abilities { get; init; } = default!;

}