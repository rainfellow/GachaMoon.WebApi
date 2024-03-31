namespace GachaMoon.Application.User.External.DiscordSignup;

public class DiscordSignUpCommand : IRequest<DiscordSignUpCommandResult>
{
    public string DiscordIdentifier { get; init; } = default!;
    public string AccountName { get; init; } = default!;

    public DiscordSignUpCommand(string discordIdentifier, string accountName)
    {
        DiscordIdentifier = discordIdentifier;
        AccountName = accountName;
    }
}