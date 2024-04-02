namespace GachaMoon.Application.User.External.DiscordSignup;

public class DiscordSignUpCommand(string discordIdentifier, string accountName) : IRequest<DiscordSignUpCommandResult>
{
    public string DiscordIdentifier { get; init; } = discordIdentifier;
    public string AccountName { get; init; } = accountName;
}