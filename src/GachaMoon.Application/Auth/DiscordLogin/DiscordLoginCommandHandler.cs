using GachaMoon.Database;
using GachaMoon.Utilities.Jwt;
using GachaMoon.Common.Query;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Domain.Users;
using GachaMoon.Common.Exceptions.Users;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Application.Auth.Common;
using GachaMoon.Services.Abstractions.Clients.Data;
using GachaMoon.Services.Abstractions.Auth;

namespace GachaMoon.Application.Auth.DiscordLogin;

public class DiscordLoginCommandHandler(ApplicationDbContext dbContext, JwtOptions jwtOptions, IDiscordApiClient discordApiClient, IDiscordSignupService discordSignupService) : IRequestHandler<DiscordLoginCommand, DiscordLoginCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly JwtOptions _jwtOptions = jwtOptions;
    private readonly IDiscordApiClient _discordApiClient = discordApiClient;
    private readonly IDiscordSignupService _discordSignupService = discordSignupService;

    public async Task<DiscordLoginCommandResult> Handle(DiscordLoginCommand request, CancellationToken cancellationToken)
    {
        var discordUser = await _discordApiClient.CheckUserCredentials(request.Code, cancellationToken);
        var user = await _dbContext.ExternalUsers
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Identifier == discordUser.Id && x.UserType == ExternalUserType.Discord, cancellationToken)
            ?? await RegisterNewDiscordUser(discordUser);

        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == user.AccountId, cancellationToken)
            ?? throw new UserNotFoundException(user.Id);

        var token = JwtUtilities
            .GenerateToken(new JwtTokenData(user.Id, user.AccountId, account.AccountType == Domain.Accounts.AccountType.Admin, false), _jwtOptions);

        return new DiscordLoginCommandResult(new LoginData(account.Id, token));
    }

    private async Task<ExternalUser> RegisterNewDiscordUser(DiscordUserData data)
    {
        var newUser = await _discordSignupService.RegisterDiscordUser(data);
        return newUser;
    }
}
