using GachaMoon.Application.User.UserInfo;

namespace GachaMoon.WebApi.Endpoints.Application.User.Models;

public record UserInfoResponse
{
    public string Email { get; set; } = default!;

    public static UserInfoResponse FromResult(UserInfoQueryResult result)
    {
        return new UserInfoResponse
        {
            Email = result.Email
        };
    }
}
