using GachaMoon.Application.Auth.Common;
using GachaMoon.Application.Auth.DiscordLogin;
namespace GachaMoon.WebApi.Endpoints.Application.User.Models;

public record DiscordLoginResponse(LoginData LoginData)
{
    public static DiscordLoginResponse FromDiscordResult(DiscordLoginCommandResult result)
    {
        return new DiscordLoginResponse(result.LoginData);
    }
}
