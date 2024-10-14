using GachaMoon.Application.Accounts.UpdateAccountInfo;

namespace GachaMoon.WebApi.Endpoints.Application.Account.Models;

public record UpdateAccountInfoRequest
{
    public string? UpdatedAccountName { get; set; } = default!;

    public UpdateAccountInfoCommand ToCommand(long accountId)
    {
        return new UpdateAccountInfoCommand(accountId, UpdatedAccountName);
    }
}
