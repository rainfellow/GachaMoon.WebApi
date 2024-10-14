using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Accounts.UpdateAccountInfo;

public class UpdateAccountInfoCommand(long accountId, string? updatedAccountName) : IRequest<UpdateAccountInfoCommandResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public string? UpdatedAccountName { get; init; } = updatedAccountName;
}
