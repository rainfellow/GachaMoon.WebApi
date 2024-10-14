using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Quiz.ListAllAnimes;

public class ListAllAnimesQueryValidator : AbstractValidator<ListAllAnimesQuery>
{
    public ListAllAnimesQueryValidator()
    {
        Include(new AccountValidator());
    }
}