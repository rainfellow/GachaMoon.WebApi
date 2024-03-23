using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.User.UserInfo;

public class UserInfoQuery : IRequest<UserInfoQueryResult>, IUserIdRequest
{
    public long UserId { get; init; }

    public UserInfoQuery(long userId)
    {
        UserId = userId;

    }
}
