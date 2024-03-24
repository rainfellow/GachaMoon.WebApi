// src/GachaMoon.Domain/Npcs/NpcCharacterBaseStats.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Npcs;
public class NpcCharacterBaseStats : SoftDeleteEntityBase<long>
{
    public long NpcCharacterId { get; set; }
    public virtual NpcCharacter NpcCharacter { get; set; } = default!;
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int Health { get; set; }
};