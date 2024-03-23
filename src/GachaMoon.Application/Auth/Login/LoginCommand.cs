using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Auth.Login;

public class LoginCommand : IRequest<LoginCommandResult>, IEmailData
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
