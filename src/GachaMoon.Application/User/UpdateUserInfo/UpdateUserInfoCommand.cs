using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.User.UpdateUserInfo;

public class UpdateUserInfoCommand(long userId, string firstName, string lastName, string? middleName) : IRequest, IUserIdRequest
{
    public long UserId { get; init; } = userId;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
    public string? MiddleName { get; init; } = middleName;
}
