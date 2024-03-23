using GachaMoon.Application.Auth.Common;
using GachaMoon.Application.Auth.Signup;

namespace GachaMoon.WebApi.Endpoints.Application.User.Models;

public record SignupResponse(LoginData LoginData)
{
    public static SignupResponse FromResult(SignUpCommandResult result)
    {
        return new SignupResponse(result.LoginData);
    }
}
