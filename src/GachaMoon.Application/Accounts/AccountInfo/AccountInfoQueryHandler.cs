using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Accounts.AccountInfo;

public class AccountInfoQueryHandler : IRequestHandler<AccountInfoQuery, AccountInfoQueryResult>
{
    private readonly ApplicationDbContext _dbContext;

    public AccountInfoQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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

        return new()
        {
            AccountName = account.AccountName,
            AccountType = account.AccountType
        };
    }
}
