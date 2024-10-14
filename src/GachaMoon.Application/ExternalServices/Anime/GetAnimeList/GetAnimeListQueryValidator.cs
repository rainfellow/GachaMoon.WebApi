using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.ExternalServices.Anime.GetAnimeList;

public class GetAnimeListQueryValidator : AbstractValidator<GetAnimeListQuery>
{
    public GetAnimeListQueryValidator()
    {
        Include(new AccountValidator());
    }
}