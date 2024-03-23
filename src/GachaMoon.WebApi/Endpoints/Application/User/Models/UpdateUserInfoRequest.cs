using GachaMoon.Application.User.UpdateUserInfo;

namespace GachaMoon.WebApi.Endpoints.Application.User.Models;

public record UpdateUserInfoRequest(string FirstName, string LastName, string? MiddleName)
{
    public UpdateUserInfoCommand ToCommand(long userId)
    {
        return new UpdateUserInfoCommand(userId, FirstName, LastName, MiddleName);
    }
}
