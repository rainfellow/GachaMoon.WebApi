using GachaMoon.Services.Abstractions.Anime.Data;

namespace GachaMoon.Services.Abstractions.Anime;

public interface IUserSavedAnimeListService
{
    public Task<UserAnimeListData> GetAnimeList(long accountId, CancellationToken cancellationToken = default);
    public Task<ICollection<UserAnimeListData>> MassGetAnimeLists(ICollection<long> accountIds, CancellationToken cancellationToken = default);
    public void ResetAccountListCache(long accountId);
}
