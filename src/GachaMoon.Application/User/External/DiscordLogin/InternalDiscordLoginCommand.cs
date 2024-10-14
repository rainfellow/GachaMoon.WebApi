namespace GachaMoon.Application.User.External.DiscordLogin;

public class InternalDiscordLoginCommand(string discordIdentifier) : IRequest<InternalDiscordLoginCommandResult>
{
    public string DiscordIdentifier { get; init; } = discordIdentifier;
}