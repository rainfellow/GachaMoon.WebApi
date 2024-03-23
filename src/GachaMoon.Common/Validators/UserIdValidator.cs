using GachaMoon.Common.Contracts;
using FluentValidation;

namespace GachaMoon.Common.Validators;

public class UserIdValidator : AbstractValidator<IUserIdRequest>
{
    public UserIdValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
