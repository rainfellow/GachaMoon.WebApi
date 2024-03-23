// src/GachaMoon.Domain/Characters/CharacterBaseStats.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Characters;

public class CharacterBaseStats : SoftDeleteEntityBase<long>
{
    public long CharacterId { get; set; }
    public virtual Character Character { get; set; } = default!;
    public int CharacterLevel { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int Health { get; set; }
}