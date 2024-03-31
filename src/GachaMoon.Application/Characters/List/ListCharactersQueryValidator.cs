using FluentValidation;

namespace GachaMoon.Application.Characters.List;

public class ListCharactersQueryValidator : AbstractValidator<ListCharactersQuery>
{
    public ListCharactersQueryValidator()
    {
    }
}