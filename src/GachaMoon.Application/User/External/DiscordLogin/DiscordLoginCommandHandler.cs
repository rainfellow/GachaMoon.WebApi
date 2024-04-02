using GachaMoon.Database;
using GachaMoon.Utilities.Jwt;
using GachaMoon.Common.Query;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Domain.Users;
using GachaMoon.Common.Exceptions.Users;

namespace GachaMoon.Application.User.External.DiscordLogin;

public class DiscordLoginCommandHandler(ApplicationDbContext dbContext, JwtOptions jwtOptions) : IRequestHandler<DiscordLoginCommand, DiscordLoginCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly JwtOptions _jwtOptions = jwtOptions;

    public async Task<DiscordLoginCommandResult> Handle(DiscordLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.ExternalUsers
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Identifier == request.DiscordIdentifier && x.UserType == ExternalUserType.Discord, cancellationToken)
            ?? throw new UserNotFoundException(request.DiscordIdentifier);

        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == user.AccountId, cancellationToken)
            ?? throw new UserNotFoundException(user.Id);

        var token = JwtUtilities
            .GenerateToken(new JwtTokenData(user.Id, user.AccountId, account.AccountType == Domain.Accounts.AccountType.Admin, false), _jwtOptions);

        return new DiscordLoginCommandResult
        {
            Token = token,
            AccountId = account.Id,
            ExternalUserId = user.Id
        };
    }
}
