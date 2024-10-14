using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Anime.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Services.Anime;

public class UserSavedAnimeListService(IServiceScopeFactory serviceScopeFactory) : IUserSavedAnimeListService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private Dictionary<long, UserAnimeListData> CachedListDictionary { get; set; } = new Dictionary<long, UserAnimeListData>();

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
}
