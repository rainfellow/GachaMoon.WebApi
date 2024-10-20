using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IShikimoriClient
{
    public Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, ICollection<string> animeStatuses, CancellationToken cancellationToken = default);
    public Task<ShikimoriAnimeData> GetAnimeDetails(long malId, CancellationToken cancellationToken = default);
}
