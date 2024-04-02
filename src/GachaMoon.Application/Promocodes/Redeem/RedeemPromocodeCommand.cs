using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Promocodes.Redeem;

public class RedeemPromocodeCommand(long accountId, string submittedCode) : IRequest<RedeemPromocodeCommandResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public string SubmittedCode { get; init; } = submittedCode;
}
