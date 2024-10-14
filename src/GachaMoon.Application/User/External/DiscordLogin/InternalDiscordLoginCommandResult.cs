namespace GachaMoon.Application.User.External.DiscordLogin;

public record InternalDiscordLoginCommandResult
{
    public long AccountId { get; init; } = default!;
    public long ExternalUserId { get; init; } = default!;
    public string Token { get; init; } = default!;
}