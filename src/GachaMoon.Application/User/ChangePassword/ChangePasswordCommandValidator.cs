using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.User.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        Include(new UserIdValidator());
    }
}
