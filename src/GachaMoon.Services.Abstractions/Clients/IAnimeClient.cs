using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IAnimeClient
{
    public Task<AnimeData> RandomAnime(CancellationToken cancellationToken = default);
    public Task<ICollection<AnimeData>> AnimeFromQuery(string query, CancellationToken cancellationToken = default);
    public Task<AnimeData> AnimeFromId(int id, CancellationToken cancellationToken = default);
}
