namespace GachaMoon.Application.User.External.DiscordLogin;

public class DiscordLoginCommand : IRequest<DiscordLoginCommandResult>
{
    public string DiscordIdentifier { get; init; } = default!;

    public DiscordLoginCommand(string discordIdentifier)
    {
        DiscordIdentifier = discordIdentifier;
    }
}