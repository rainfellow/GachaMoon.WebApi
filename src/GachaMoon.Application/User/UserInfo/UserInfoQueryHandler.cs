using GachaMoon.Common.Exceptions.Users;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.User.UserInfo;

public class UserInfoQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<UserInfoQuery, UserInfoQueryResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<UserInfoQueryResult> Handle(UserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.InternalUsers
            .IsNotDeleted()
            .Select(x => new
            {
                x.Id,
                x.Email
            })
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new UserNotFoundException(request.UserId);

        return new()
        {
            Email = user.Email
        };
    }
}
