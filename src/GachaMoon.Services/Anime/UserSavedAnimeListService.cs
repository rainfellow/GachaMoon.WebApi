using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.ExternalServices;
using GachaMoon.Services.Abstractions.Anime;
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
                .Select(x => x.UserAnimeList)
                .FirstOrDefaultAsync() ?? new UserAnimeListData();
            CachedListDictionary.Add(accountId, dbList);
            return dbList;
        }
    }
}
