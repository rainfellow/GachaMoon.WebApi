using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Accounts.UpdateAccountInfo;

public class UpdateAccountInfoCommandValidator : AbstractValidator<UpdateAccountInfoCommand>
{
    public UpdateAccountInfoCommandValidator()
    {
        Include(new AccountValidator());
    }
}