using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IMyAnimeListApiClient
{
    public Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, ICollection<string> animeStatuses, CancellationToken cancellationToken = default);
    public Task<UserAnimeData> GetAnime(string query, CancellationToken cancellationToken = default);
    public Task<AnimeDetailedData> GetAnimeDetails(int id, CancellationToken cancellationToken = default);
}
