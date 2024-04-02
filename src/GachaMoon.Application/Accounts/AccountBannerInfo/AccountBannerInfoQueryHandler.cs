using GachaMoon.Application.Accounts.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Accounts.AccountBannerInfo;

public class AccountBannerInfoQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<AccountBannerInfoQuery, AccountBannerInfoQueryResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<AccountBannerInfoQueryResult> Handle(AccountBannerInfoQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .Select(x => new
            {
                x.Id,
                x.AccountName
            })
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken)
            ?? throw new NotImplementedException($"Account with id {request.AccountId} not found.");

        var bannerStats = await _dbContext.AccountBannerStats
            .IsNotDeleted()
            .Where(x => x.AccountId == request.AccountId)
            .Select(x => new AccountBannerData
            {
                TotalRolls = x.TotalRolls,
                TotalEpicRolls = x.TotalEpicRolls,
                TotalLegendaryRolls = x.TotalLegendaryRolls,
                RollsToLegendary = x.RollsToLegendary,
                RollsToEpic = x.RollsToEpic,
                BannerType = x.BannerType
            })
            .ToListAsync(cancellationToken);

        return new()
        {
            AccountName = account.AccountName,
            AccountBannerData = bannerStats
        };
    }
}
