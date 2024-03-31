using GachaMoon.Database;
using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Users;

namespace GachaMoon.Application.User.External.DiscordSignup;

public class DiscordSignUpCommandHandler : IRequestHandler<DiscordSignUpCommand, DiscordSignUpCommandResult>
{
    private readonly ApplicationDbContext _dbContext;

    public DiscordSignUpCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DiscordSignUpCommandResult> Handle(DiscordSignUpCommand request, CancellationToken cancellationToken)
    {
        var newAccount = CreateAccount(request);
        var newDiscordUser = CreateExternalUser(request, newAccount);
        var newAccountPremiumInventory = CreatePremiumInventory(newAccount);

        _dbContext.Accounts.Add(newAccount);
        _dbContext.ExternalUsers.Add(newDiscordUser);
        _dbContext.PremiumInventories.Add(newAccountPremiumInventory);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DiscordSignUpCommandResult();
    }

    private static Account CreateAccount(DiscordSignUpCommand request)
    {
        return new Account
        {
            AccountName = request.AccountName,
            AccountType = AccountType.User
        };
    }

    private static ExternalUser CreateExternalUser(DiscordSignUpCommand request, Account connectedAccount)
    {
        return new ExternalUser
        {
            UserType = ExternalUserType.Discord,
            Identifier = request.DiscordIdentifier,
            Account = connectedAccount
        };
    }

    private static PremiumInventory CreatePremiumInventory(Account connectedAccount)
    {
        return new PremiumInventory
        {
            Account = connectedAccount,
            PremiumCurrencyAmount = 0,
            WildcardSkillItemCount = 0
        };
    }
}
