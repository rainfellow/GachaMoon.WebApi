using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Gacha.Roll;
public class RollGachaCommandValidator : AbstractValidator<RollGachaCommand>
{
    public RollGachaCommandValidator()
    {
        Include(new AccountValidator());
        RuleFor(command => command.RollCount).GreaterThan(0);
    }
}