using GachaMoon.Domain.Users;
using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Auth;

public interface IDiscordSignupService
{
    public Task<ExternalUser> RegisterDiscordUser(DiscordUserData discordUserData, CancellationToken cancellationToken = default);
}
