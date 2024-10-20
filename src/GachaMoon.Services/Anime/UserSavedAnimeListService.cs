using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Anime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserAnimeListData = GachaMoon.Services.Abstractions.Anime.Data.UserAnimeListData;

namespace GachaMoon.Services.Anime;

public class UserSavedAnimeListService : IUserSavedAnimeListService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Dictionary<long, UserAnimeListData> CachedListDictionary { get; set; } = new Dictionary<long, UserAnimeListData>();

    private Dictionary<long, long> AnimeMALIdToInternalIdDictionary { get; set; } = new Dictionary<long, long>();
    private Dictionary<long, long> AnimeAnilistIdToInternalIdDictionary { get; set; } = new Dictionary<long, long>();

    public UserSavedAnimeListService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var availableAnimes = dbContext.Animes.Select(x => new
        {
            MyAnimeListId = x.AnimeBaseId,
            AnilistId = x.AnilistId,
            InternalId = x.Id,
        }).ToList();
        foreach (var anime in availableAnimes)
        {
            AnimeMALIdToInternalIdDictionary.TryAdd(anime.MyAnimeListId, anime.InternalId);
            AnimeAnilistIdToInternalIdDictionary.TryAdd(anime.AnilistId, anime.InternalId);
        }
    }

    public async Task<UserAnimeListData> GetAnimeList(long accountId, CancellationToken cancellationToken = default)
    {
        if (CachedListDictionary.ContainsKey(accountId))
        {
            return CachedListDictionary[accountId];
        }
        else
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var dbList = await dbContext.AccountExternalServices
                .IsNotDeleted()
                .Where(x => x.AccountId == accountId)
                .Select(x => new UserAnimeListData { AnimeListServiceProvider = x.ExternalServiceProvider, UserAnimes = x.UserAnimeList.UserAnimes, SelectedAnimeGroups = x.UserAnimeList.UserAnimeGroups, AnimeListUserId = x.ExternalServiceUserId })
                .FirstOrDefaultAsync() ?? new UserAnimeListData();

            dbList.UserAnimes = ConvertListToInternalIds(dbList.UserAnimes, dbList.AnimeListServiceProvider);
            CachedListDictionary.Add(accountId, dbList);
            return dbList;
        }
    }

    public async Task<ICollection<UserAnimeListData>> MassGetAnimeLists(ICollection<long> accountIds, CancellationToken cancellationToken = default)
    {
        var result = new List<UserAnimeListData>();
        var notCachedIds = new List<long>();

        foreach (var accountId in accountIds)
        {
            if (CachedListDictionary.ContainsKey(accountId))
            {
                result.Add(CachedListDictionary[accountId]);
            }
            else
            {
                notCachedIds.Add(accountId);
            }
        }

        if (notCachedIds.Any())
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var dbLists = await dbContext.AccountExternalServices
                .IsNotDeleted()
                .Where(x => notCachedIds.Contains(x.AccountId))
                .Select(x => new { x.AccountId, x.UserAnimeList, x.ExternalServiceProvider, x.ExternalServiceUserId })
                .ToListAsync();

            foreach (var list in dbLists)
            {
                list.UserAnimeList.UserAnimes = ConvertListToInternalIds(list.UserAnimeList.UserAnimes, list.ExternalServiceProvider);
                CachedListDictionary.Add(list.AccountId, new UserAnimeListData { AnimeListServiceProvider = list.ExternalServiceProvider, UserAnimes = list.UserAnimeList.UserAnimes, SelectedAnimeGroups = list.UserAnimeList.UserAnimeGroups, AnimeListUserId = list.ExternalServiceUserId });
                result.Add(CachedListDictionary[list.AccountId]);
            }
        }

        return result;
    }

    public void ResetAccountListCache(long accountId)
    {
        CachedListDictionary.Remove(accountId);
    }

    private List<UserAnimeData> ConvertListToInternalIds(ICollection<UserAnimeData> list, ExternalServiceProvider serviceProvider)
    {
        return serviceProvider switch
        {
            ExternalServiceProvider.None => throw new NotImplementedException(),
            ExternalServiceProvider.Shikimori => list
                .Select(x => new UserAnimeData
                {
                    Id = AnimeMALIdToInternalIdDictionary.TryGetValue(x.Id, out var internalId) ? internalId : 0,
                })
                .Where(x => x.Id != 0)
                .ToList(),
            ExternalServiceProvider.MyAnimeList => list
                .Select(x => new UserAnimeData
                {
                    Id = AnimeMALIdToInternalIdDictionary.TryGetValue(x.Id, out var internalId) ? internalId : 0,
                })
                .Where(x => x.Id != 0)
                .ToList(),
            ExternalServiceProvider.Anilist => list
                .Select(x => new UserAnimeData
                {
                    Id = AnimeAnilistIdToInternalIdDictionary.TryGetValue(x.Id, out var internalId) ? internalId : 0,
                })
                .Where(x => x.Id != 0)
                .ToList(),
            _ => throw new NotImplementedException(),
        };
    }
}
