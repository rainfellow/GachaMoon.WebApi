using System.Text.Json.Serialization;

namespace GachaMoon.Clients.DiscordApi.Data;
public record DiscordTokenData
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
}