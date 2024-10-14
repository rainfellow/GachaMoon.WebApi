using GachaMoon.Database;
using GachaMoon.Utilities.Jwt;
using GachaMoon.Common.DatabaseExtensions;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Utilities.Formatted;
using GachaMoon.Utilities;
using GachaMoon.Common.Exceptions.Login;
using GachaMoon.Common.Query;
using GachaMoon.Common.Exceptions.Users;

namespace GachaMoon.Application.Auth.Login;

public class LoginCommandHandler(ApplicationDbContext dbContext, JwtOptions jwtOptions) : IRequestHandler<LoginCommand, LoginCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly JwtOptions _jwtOptions = jwtOptions;

    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.InternalUsers
            .FilterUserByEmail(new FormattedEmail(request.Email))
            .IsNotDeleted()
            .Select(x => new { x.Id, x.Password, x.AccountId })
            .SingleOrDefaultAsync(cancellationToken);

        if (user == null ||
            !PasswordUtilities.VerifyPassword(request.Password, user.Password))
        {
            throw new LoginFailedException();
        }
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == user.AccountId, cancellationToken)
            ?? throw new UserNotFoundException(user.Id);

        var token = JwtUtilities
            .GenerateToken(new JwtTokenData(user.Id, user.AccountId, account.AccountType == Domain.Accounts.AccountType.Admin, false), _jwtOptions);

        return new(new(account.Id, token));
    }

    // public async Task CreateUserIfNotExists()
    // {
    //     var user = await _dbContext.InternalUsers
    //         .FilterUserByEmail(new FormattedEmail("tester2@gachamoon.xyz"))
    //         .IsNotDeleted()
    //         .Select(x => new { x.Id, x.Password, x.AccountId })
    //         .FirstOrDefaultAsync();
    //     if (user == null)
    //     {
    //         _dbContext.InternalUsers.Add(new InternalUser
    //         {
    //             Email = "tester2@gachamoon.xyz",
    //             Password = PasswordUtilities.GeneratePasswordBytes("test!1234!antk"),
    //             AccountId = 1111112
    //         });
    //         await _dbContext.SaveChangesAsync();
    //     }
    // }
}
