namespace GachaMoon.Application.User.External.DiscordLogin;

public class DiscordLoginCommand(string discordIdentifier) : IRequest<DiscordLoginCommandResult>
{
    public string DiscordIdentifier { get; init; } = discordIdentifier;
}