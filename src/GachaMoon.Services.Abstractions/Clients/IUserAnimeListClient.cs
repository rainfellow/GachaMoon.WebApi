using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IUserAnimeListClient
{
    public Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, CancellationToken cancellationToken = default);
}
