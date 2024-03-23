using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Auth.Signup;

public class SignUpCommand : IRequest<SignUpCommandResult>, IEmailData
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}
