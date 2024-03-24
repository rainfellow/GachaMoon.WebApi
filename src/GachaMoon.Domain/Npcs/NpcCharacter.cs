// src/GachaMoon.Domain/Npcs/NpcCharacter.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Npcs;
public class NpcCharacter : SoftDeleteEntityBase<long>
{
    public string Name { get; set; } = default!;
    public NpcType NpcType { get; set; }
    public int ChallengeRating { get; set; }
};
