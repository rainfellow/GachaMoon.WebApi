using GachaMoon.Application.Auth.Common;
using GachaMoon.Application.Auth.Login;

namespace GachaMoon.WebApi.Endpoints.Application.Login.Models;

public record LoginResponse(LoginData LoginData)
{
    public static LoginResponse FromResult(LoginCommandResult result)
    {
        return new LoginResponse(result.LoginData);
    }
}
