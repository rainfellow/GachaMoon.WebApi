using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IDiscordApiClient
{
    public Task<DiscordUserData> CheckUserCredentials(string code, CancellationToken cancellationToken = default);
}
