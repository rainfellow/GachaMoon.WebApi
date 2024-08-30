using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IUserAnimeListClient
{
    public Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, CancellationToken cancellationToken = default);
    public Task<UserAnimeData> GetAnime(string query, CancellationToken cancellationToken = default);
    public Task<UserAnimeData> GetAnimeDetails(int id, CancellationToken cancellationToken = default);
}
