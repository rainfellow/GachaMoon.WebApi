using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Accounts.AccountInfo;

public class AccountInfoQueryValidator : AbstractValidator<AccountInfoQuery>
{
    public AccountInfoQueryValidator()
    {
        Include(new AccountValidator());
    }
}