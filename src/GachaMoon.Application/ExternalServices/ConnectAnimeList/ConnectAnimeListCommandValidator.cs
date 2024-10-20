using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.ExternalServices.ConnectAnimeList;

public class ConnectAnimeListCommandValidator : AbstractValidator<ConnectAnimeListCommand>
{
    public ConnectAnimeListCommandValidator()
    {
        Include(new AccountValidator());
    }
}