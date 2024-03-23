using GachaMoon.Utilities.Sorting;
using FluentValidation;

namespace GachaMoon.Common.Validators;

public class SortingContainerValidator : AbstractValidator<ISortingContainer>
{
    public SortingContainerValidator()
    {
        RuleFor(e => e).Custom(ValidateSortings);
    }

    private static void ValidateSortings(ISortingContainer sortingContainer, ValidationContext<ISortingContainer> context)
    {
        try
        {
            SortingsHelper.ValidateSortings(sortingContainer);
        }
        catch (Exception e)
        {
            context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(sortingContainer.Sortings), e.Message));
        }
    }
}