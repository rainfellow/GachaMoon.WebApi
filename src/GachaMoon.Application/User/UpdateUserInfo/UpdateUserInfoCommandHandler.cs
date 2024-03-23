using GachaMoon.Common.Exceptions.Users;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.User.UpdateUserInfo;

public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateUserInfoCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.InternalUsers
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new UserNotFoundException(request.UserId);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
