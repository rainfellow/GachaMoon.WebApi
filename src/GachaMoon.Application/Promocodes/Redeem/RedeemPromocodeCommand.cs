using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Promocodes.Redeem;

public class RedeemPromocodeCommand : IRequest<RedeemPromocodeCommandResult>, IAccountRequest
{
    public long AccountId { get; init; }
    public string SubmittedCode { get; init; }

    public RedeemPromocodeCommand(long accountId, string submittedCode)
    {
        AccountId = accountId;
        SubmittedCode = submittedCode;
    }
}
