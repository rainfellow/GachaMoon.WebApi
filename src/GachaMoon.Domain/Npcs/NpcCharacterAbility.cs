// src/GachaMoon.Domain/Npcs/NpcCharacterAbility.cs
using GachaMoon.Domain.Base;
using GachaMoon.Domain.Characters;

namespace GachaMoon.Domain.Npcs;
public class NpcCharacterAbility : SoftDeleteEntityBase<long>
{
    public long NpcCharacterId { get; set; }
    public virtual NpcCharacter NpcCharacter { get; set; } = default!;
    public long CharacterAbilityId { get; set; }
    public virtual CharacterAbility CharacterAbility { get; set; } = default!;
};
