using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.ExternalServices.ConnectExternalService;

public class ConnectExternalServiceCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<ConnectExternalServiceCommand, ConnectExternalServiceCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ConnectExternalServiceCommandResult> Handle(ConnectExternalServiceCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken) ?? throw new NotImplementedException();

        var connectedService = await _dbContext.AccountExternalServices
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.AccountId == account.Id && x.ExternalServiceType == request.ServiceType, cancellationToken);

        if (connectedService is not null)
        {
            connectedService.ExternalServiceProvider = request.ServiceProvider;
            connectedService.ExternalServiceUserId = request.ExternalServiceUserId;
        }
        else
        {
            _dbContext.AccountExternalServices.Add(new AccountConnectedExternalService
            {
                AccountId = account.Id,
                ExternalServiceType = request.ServiceType,
                ExternalServiceProvider = request.ServiceProvider,
                ExternalServiceUserId = request.ExternalServiceUserId,
            });
        }

        await _dbContext.SaveChangesAsync();
        return new ConnectExternalServiceCommandResult();
    }
}