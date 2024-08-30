using GachaMoon.Domain.ExternalServices;

namespace GachaMoon.Services.Abstractions.Anime;

public interface IUserSavedAnimeListService
{
    public Task<UserAnimeListData> GetAnimeList(long accountId, CancellationToken cancellationToken = default);
}
