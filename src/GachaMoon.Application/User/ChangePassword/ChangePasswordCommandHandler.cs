using GachaMoon.Common.Exceptions.Users;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Utilities;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.User.ChangePassword;

public class ChangePasswordCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<ChangePasswordCommand>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.InternalUsers
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
            ?? throw new UserNotFoundException(request.UserId);

        user.Password = PasswordUtilities.VerifyPassword(request.OldPassword, user.Password)
            ? PasswordUtilities.GeneratePasswordBytes(request.NewPassword)
            : throw new WrongPasswordException();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
