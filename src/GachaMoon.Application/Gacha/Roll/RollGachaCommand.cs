using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Gacha.Roll;
public class RollGachaCommand(long accountId, long bannerId, int rollCount) : IRequest<RollGachaCommandResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public long BannerId { get; init; } = bannerId;
    public int RollCount { get; set; } = rollCount;
}
