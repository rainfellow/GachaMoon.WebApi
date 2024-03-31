using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Promocodes.Redeem;

public class RedeemPromocodeCommandValidator : AbstractValidator<RedeemPromocodeCommand>
{
    public RedeemPromocodeCommandValidator()
    {
        Include(new AccountValidator());
    }
}