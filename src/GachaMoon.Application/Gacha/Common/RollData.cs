namespace GachaMoon.Application.Gacha.Common;
public record RollData
{
    public RollResult Result { get; init; }
    public long? CharacterId { get; init; }
}