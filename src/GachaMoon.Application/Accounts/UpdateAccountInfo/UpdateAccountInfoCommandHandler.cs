using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Accounts.UpdateAccountInfo;

public class UpdateAccountInfoCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateAccountInfoCommand, UpdateAccountInfoCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<UpdateAccountInfoCommandResult> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken)
            ?? throw new NotImplementedException($"Account with id {request.AccountId} not found.");

        if (request.UpdatedAccountName != null && request.UpdatedAccountName != account.AccountName)
        {
            account.AccountName = request.UpdatedAccountName;
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new();
    }
}
