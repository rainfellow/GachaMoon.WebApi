using FluentValidation;
using GachaMoon.Common.Contracts;

namespace GachaMoon.Common.Validators;

public class AdminValidator : AbstractValidator<IAdminRequest>
{
    public AdminValidator()
    {
        Include(new UserIdValidator());
    }
}
