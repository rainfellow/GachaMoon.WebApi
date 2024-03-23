using GachaMoon.Application.Auth.Signup;

namespace GachaMoon.WebApi.Endpoints.Application.User.Models;

public record SignupRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

    public SignUpCommand ToCommand()
    {
        return new SignUpCommand
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };
    }
}
