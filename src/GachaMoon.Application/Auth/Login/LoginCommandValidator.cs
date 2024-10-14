using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        Include(new EmailValidator());

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
