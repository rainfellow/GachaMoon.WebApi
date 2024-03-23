using GachaMoon.Common.Contracts;
using FluentValidation;

namespace GachaMoon.Common.Validators;

public class EmailValidator : AbstractValidator<IEmailData>
{
    public EmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email address is required.")
            .EmailAddress()
                .WithMessage("A valid email address is required.");
    }
}
