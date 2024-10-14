namespace GachaMoon.Application.Auth.DiscordLogin;

public class DiscordLoginCommand(string code) : IRequest<DiscordLoginCommandResult>
{
    public string Code { get; init; } = code;
}
