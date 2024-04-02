using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.User.UserInfo;

public class UserInfoQuery(long userId) : IRequest<UserInfoQueryResult>, IUserIdRequest
{
    public long UserId { get; init; } = userId;
}
