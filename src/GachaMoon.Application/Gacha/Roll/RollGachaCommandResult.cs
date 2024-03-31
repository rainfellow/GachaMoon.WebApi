using GachaMoon.Application.Gacha.Common;

namespace GachaMoon.Application.Gacha.Roll;
public class RollGachaCommandResult
{
    public ICollection<RollData> RollDataCollection { get; init; }

    public RollGachaCommandResult(ICollection<RollData> rollDataCollection)
    {
        RollDataCollection = rollDataCollection;
    }
}