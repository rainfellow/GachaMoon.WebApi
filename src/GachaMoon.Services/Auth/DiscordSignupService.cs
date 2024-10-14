using GachaMoon.Database;
using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Users;
using GachaMoon.Services.Abstractions.Auth;
using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Auth;

public class DiscordSignupService(ApplicationDbContext dbContext) : IDiscordSignupService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ExternalUser> RegisterDiscordUser(DiscordUserData discordUserData, CancellationToken cancellationToken = default)
    {
        var newAccount = CreateAccount(discordUserData);
        var newDiscordUser = CreateExternalUser(discordUserData, newAccount);
        var newAccountPremiumInventory = CreatePremiumInventory(newAccount);

        _dbContext.Accounts.Add(newAccount);
        _dbContext.ExternalUsers.Add(newDiscordUser);
        _dbContext.PremiumInventories.Add(newAccountPremiumInventory);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return newDiscordUser;
    }

    private static Account CreateAccount(DiscordUserData discordUserData)
    {
        return new Account
        {
            AccountName = discordUserData.Username,
            AccountType = AccountType.User
        };
    }

    private static ExternalUser CreateExternalUser(DiscordUserData discordUserData, Account connectedAccount)
    {
        return new ExternalUser
        {
            UserType = ExternalUserType.Discord,
            Identifier = discordUserData.Id,
            Account = connectedAccount
        };
    }

    private static PremiumInventory CreatePremiumInventory(Account connectedAccount)
    {
        return new PremiumInventory
        {
            Account = connectedAccount,
            PremiumCurrencyAmount = 0,
            WildcardSkillItemCount = 0,
            StandardBannerRollsAmount = 0
        };
    }
}
