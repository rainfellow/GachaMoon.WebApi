using System.Text.Json.Serialization;

namespace GachaMoon.Clients.DiscordApi.Data;
public record DiscordTokenRequestData
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = default!;

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; } = default!;

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = default!;
    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; set; } = default!;
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;
}