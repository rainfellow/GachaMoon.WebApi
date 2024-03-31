using GachaMoon.Common.Contracts;
using FluentValidation;

namespace GachaMoon.Common.Validators;

public class AccountValidator : AbstractValidator<IAccountRequest>
{
    public AccountValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty();
    }
}
