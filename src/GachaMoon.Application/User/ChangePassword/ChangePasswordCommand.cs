using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.User.ChangePassword;

public class ChangePasswordCommand(long userId, string oldPassword, string newPassword) : IRequest, IUserIdRequest
{
    public long UserId { get; init; } = userId;
    public string OldPassword { get; init; } = oldPassword;
    public string NewPassword { get; init; } = newPassword;
}
