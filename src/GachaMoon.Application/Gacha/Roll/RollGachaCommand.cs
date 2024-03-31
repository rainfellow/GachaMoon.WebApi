using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Gacha.Roll;
public class RollGachaCommand : IRequest<RollGachaCommandResult>, IAccountRequest
{
    public long AccountId { get; init; }
    public long BannerId { get; init; }
    public int RollCount { get; set; }

    public RollGachaCommand(long accountId, long bannerId, int rollCount)
    {
        AccountId = accountId;
        BannerId = bannerId;
        RollCount = rollCount;
    }
}
