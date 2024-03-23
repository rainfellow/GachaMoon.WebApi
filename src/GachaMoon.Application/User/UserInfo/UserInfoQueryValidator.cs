using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.User.UserInfo;

public class UserInfoQueryValidator : AbstractValidator<UserInfoQuery>
{
    public UserInfoQueryValidator()
    {
        Include(new UserIdValidator());
    }
}
