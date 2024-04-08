using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.ExternalServices.ConnectExternalService;

public class ConnectExternalServiceCommandValidator : AbstractValidator<ConnectExternalServiceCommand>
{
    public ConnectExternalServiceCommandValidator()
    {
        Include(new AccountValidator());
    }
}