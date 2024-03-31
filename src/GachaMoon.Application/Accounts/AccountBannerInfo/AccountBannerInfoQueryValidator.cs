using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Accounts.AccountBannerInfo;

public class AccountBannerInfoQueryValidator : AbstractValidator<AccountBannerInfoQuery>
{
    public AccountBannerInfoQueryValidator()
    {
        Include(new AccountValidator());
    }
}