using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Clients;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.ExternalServices.ConnectExternalService;

public class ConnectExternalServiceCommandHandler(ApplicationDbContext dbContext, IUserAnimeListClient userAnimeListClient, IUserSavedAnimeListService userSavedAnimeListService) : IRequestHandler<ConnectExternalServiceCommand, ConnectExternalServiceCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IUserAnimeListClient _userAnimeListClient = userAnimeListClient;
    private readonly IUserSavedAnimeListService _userSavedAnimeListService = userSavedAnimeListService;


    public async Task<ConnectExternalServiceCommandResult> Handle(ConnectExternalServiceCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken) ?? throw new NotImplementedException();

        var connectedService = await _dbContext.AccountExternalServices
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.AccountId == account.Id && x.ExternalServiceType == request.ServiceType, cancellationToken);

        var animeList = await GetMALAnimeList(request);

        if (connectedService is not null)
        {
            connectedService.ExternalServiceProvider = request.ServiceProvider;
            connectedService.ExternalServiceUserId = request.ExternalServiceUserId;
            connectedService.UserAnimeList = animeList;
        }
        else
        {
            _dbContext.AccountExternalServices.Add(new AccountConnectedExternalService
            {
                AccountId = account.Id,
                ExternalServiceType = request.ServiceType,
                ExternalServiceProvider = request.ServiceProvider,
                ExternalServiceUserId = request.ExternalServiceUserId,
                UserAnimeList = animeList
            });
        }

        await _dbContext.SaveChangesAsync();

        _userSavedAnimeListService.ResetAccountListCache(account.Id);

        return new ConnectExternalServiceCommandResult
        {
            ExternalServiceUserId = request.ExternalServiceUserId,
            AnimeCount = animeList.UserAnimes.Count
        };
    }

    private async Task<UserAnimeListData> GetMALAnimeList(ConnectExternalServiceCommand request)
    {
        var animeList = await _userAnimeListClient.GetUserAnimeList(request.ExternalServiceUserId, request.AllowedListGroups);
        return new UserAnimeListData
        {
            UserAnimes = animeList.Select(x => new UserAnimeData
            {
                Id = x.Id,
            }).ToList(),
            UserAnimeGroups = request.AllowedListGroups
        };
    }
}