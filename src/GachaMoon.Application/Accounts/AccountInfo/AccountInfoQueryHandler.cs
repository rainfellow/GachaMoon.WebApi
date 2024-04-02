using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Accounts.AccountInfo;

public class AccountInfoQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<AccountInfoQuery, AccountInfoQueryResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<AccountInfoQueryResult> Handle(AccountInfoQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .Select(x => new
            {
                x.Id,
                x.AccountName,
                x.AccountType
            })
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken)
            ?? throw new NotImplementedException($"Account with id {request.AccountId} not found.");

        var accountPremiumInventory = await _dbContext.PremiumInventories
            .IsNotDeleted()
            .Where(x => x.AccountId == account.Id)
            .Select(x => new
            {
                x.PremiumCurrencyAmount,
                x.WildcardSkillItemCount,
                x.StandardBannerRollsAmount
            })
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotImplementedException();

        return new()
        {
            AccountName = account.AccountName,
            AccountType = account.AccountType,
            PremiumCurrencyAmount = accountPremiumInventory.PremiumCurrencyAmount,
            WildcardSkillItemCount = accountPremiumInventory.WildcardSkillItemCount,
            StandardBannerRollsAmount = accountPremiumInventory.StandardBannerRollsAmount
        };
    }
}
