using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.User.UpdateUserInfo;

public class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoCommandValidator()
    {
        Include(new UserIdValidator());
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(30);
    }
}
