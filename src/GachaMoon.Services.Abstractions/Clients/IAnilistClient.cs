using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IAnilistClient
{
    public Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, ICollection<string> animeStatuses, CancellationToken cancellationToken = default);
}
