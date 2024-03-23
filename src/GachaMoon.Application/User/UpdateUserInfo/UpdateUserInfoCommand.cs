using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.User.UpdateUserInfo;

public class UpdateUserInfoCommand : IRequest, IUserIdRequest
{
    public long UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }

    public UpdateUserInfoCommand(long userId, string firstName, string lastName, string? middleName)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
}
