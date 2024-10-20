using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Clients;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.ExternalServices.ConnectAnimeList;

public class ConnectAnimeListCommandHandler(ApplicationDbContext dbContext, IMyAnimeListApiClient userAnimeListClient, IAnilistClient anilistClient, IShikimoriClient shikimoriClient, IUserSavedAnimeListService userSavedAnimeListService)
 : IRequestHandler<ConnectAnimeListCommand, ConnectAnimeListCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMyAnimeListApiClient _userAnimeListClient = userAnimeListClient;
    private readonly IAnilistClient _anilistClient = anilistClient;
    private readonly IShikimoriClient _shikimoriClient = shikimoriClient;
    private readonly IUserSavedAnimeListService _userSavedAnimeListService = userSavedAnimeListService;


    public async Task<ConnectAnimeListCommandResult> Handle(ConnectAnimeListCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken) ?? throw new NotImplementedException();

        var connectedService = await _dbContext.AccountExternalServices
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.AccountId == account.Id && x.ExternalServiceType == request.ServiceType, cancellationToken);

        var animeList = request.ServiceProvider switch
        {
            ExternalServiceProvider.None => throw new NotImplementedException(),
            ExternalServiceProvider.Shikimori => await GetShikimoriAnimeList(request),
            ExternalServiceProvider.MyAnimeList => await GetMALAnimeList(request),
            ExternalServiceProvider.Anilist => await GetAnilistAnimeList(request),
            _ => throw new NotImplementedException(),
        };


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

        return new ConnectAnimeListCommandResult
        {
            ExternalServiceUserId = request.ExternalServiceUserId,
            AnimeCount = animeList.UserAnimes.Count
        };
    }

    private async Task<UserAnimeListData> GetMALAnimeList(ConnectAnimeListCommand request)
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

    private async Task<UserAnimeListData> GetAnilistAnimeList(ConnectAnimeListCommand request)
    {
        var animeList = await _anilistClient.GetUserAnimeList(request.ExternalServiceUserId, request.AllowedListGroups);
        return new UserAnimeListData
        {
            UserAnimes = animeList.Select(x => new UserAnimeData
            {
                Id = x.Id,
            }).ToList(),
            UserAnimeGroups = request.AllowedListGroups
        };
    }

    private async Task<UserAnimeListData> GetShikimoriAnimeList(ConnectAnimeListCommand request)
    {
        var animeList = await _shikimoriClient.GetUserAnimeList(request.ExternalServiceUserId, request.AllowedListGroups);
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