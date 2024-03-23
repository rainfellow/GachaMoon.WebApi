using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.User.ChangePassword;

public class ChangePasswordCommand : IRequest, IUserIdRequest
{
    public long UserId { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }


    public ChangePasswordCommand(long userId, string oldPassword, string newPassword)
    {
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}
