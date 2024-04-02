using GachaMoon.Application.Gacha.Common;

namespace GachaMoon.Application.Gacha.Roll;
public class RollGachaCommandResult(ICollection<RollData> rollDataCollection)
{
    public ICollection<RollData> RollDataCollection { get; init; } = rollDataCollection;
}