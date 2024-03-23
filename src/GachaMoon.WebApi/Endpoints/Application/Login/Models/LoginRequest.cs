using GachaMoon.Application.Auth.Login;

namespace GachaMoon.WebApi.Endpoints.Application.Login.Models;

public record LoginRequest(string Email, string Password)
{
    public LoginCommand ToCommand()
    {
        return new LoginCommand
        {
            Email = Email,
            Password = Password
        };
    }
}
