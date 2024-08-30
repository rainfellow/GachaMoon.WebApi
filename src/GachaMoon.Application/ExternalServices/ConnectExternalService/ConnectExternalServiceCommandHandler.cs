using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Clients;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.ExternalServices.ConnectExternalService;

public class ConnectExternalServiceCommandHandler(ApplicationDbContext dbContext, IUserAnimeListClient userAnimeListClient) : IRequestHandler<ConnectExternalServiceCommand, ConnectExternalServiceCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IUserAnimeListClient _userAnimeListClient = userAnimeListClient;

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
            connectedService.UserAnimeList = await GetMALAnimeList(request.ExternalServiceUserId);
        }
        else
        {
            _dbContext.AccountExternalServices.Add(new AccountConnectedExternalService
            {
                AccountId = account.Id,
                ExternalServiceType = request.ServiceType,
                ExternalServiceProvider = request.ServiceProvider,
                ExternalServiceUserId = request.ExternalServiceUserId,
                UserAnimeList = await GetMALAnimeList(request.ExternalServiceUserId)
            });
        }

        await _dbContext.SaveChangesAsync();
        return new ConnectExternalServiceCommandResult
        {
            ExternalServiceUserId = request.ExternalServiceUserId
        };
    }

    private async Task<UserAnimeListData> GetMALAnimeList(string username)
    {
        var animeList = await _userAnimeListClient.GetUserAnimeList(username);
        return new UserAnimeListData
        {
            UserAnimes = animeList.Select(x => new UserAnimeData
            {
                Id = x.Id,
                Title = x.Title
            }).ToList()
        };
    }
}