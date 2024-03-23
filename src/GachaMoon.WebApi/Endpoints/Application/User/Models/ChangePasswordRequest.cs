using GachaMoon.Application.User.ChangePassword;

namespace GachaMoon.WebApi.Endpoints.Application.User.Models;

public record ChangePasswordRequest(string OldPassword, string NewPassword)
{

    public ChangePasswordCommand ToCommand(long userId)
    {
        return new ChangePasswordCommand(userId, OldPassword, NewPassword);
    }
}
