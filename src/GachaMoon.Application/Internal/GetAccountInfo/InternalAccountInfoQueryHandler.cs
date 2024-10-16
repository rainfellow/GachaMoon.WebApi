using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Internal.GetAccountInfo;
public class InternalAccountInfoQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<InternalAccountInfoQuery, InternalAccountInfoQueryResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<InternalAccountInfoQueryResult> Handle(InternalAccountInfoQuery request, CancellationToken cancellationToken)
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

        return new()
        {
            AccountName = account.AccountName,
            AccountId = account.Id,
        };
    }
}
