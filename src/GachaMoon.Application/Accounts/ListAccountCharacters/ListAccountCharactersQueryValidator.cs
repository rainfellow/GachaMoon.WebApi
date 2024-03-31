using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Accounts.ListAccountCharacters;

public class ListAccountCharactersQueryValidator : AbstractValidator<ListAccountCharactersQuery>
{
    public ListAccountCharactersQueryValidator()
    {
        Include(new AccountValidator());
    }
}