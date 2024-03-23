using GachaMoon.Domain.Base;
using GachaMoon.Domain.Characters;

namespace GachaMoon.Domain.Accounts;

public class AccountCharacter : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public long CharacterId { get; set; }
    public int CharacterLevel { get; set; }
    public int CharacterExperience { get; set; }
    public int RepeatCount { get; set; }
    public int TotalSkillPoints { get; set; }
    public int FreeSkillPoints { get; set; }
    public string SkillTree { get; set; } = default!;

    public virtual Account Account { get; set; } = default!;
    public virtual Character Character { get; set; } = default!;
}