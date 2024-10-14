namespace GachaMoon.Services.Abstractions.Clients.Data;
public record DiscordUserData
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
}