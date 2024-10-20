using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IAnisongDBClient
{
    public Task<ICollection<AnisongSongData>> GetAnimeSongsData(string amqAnimeName, CancellationToken cancellationToken = default);
}
