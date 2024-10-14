using System.Text.Json.Serialization;

namespace GachaMoon.Services.Abstractions.Clients.Data;
public record AnimeQuizResult
{
    [JsonPropertyName("anime_title")]
    public string AnimeTitle { get; set; } = default!;

    [JsonPropertyName("anime_id")]
    public long AnimeId { get; set; } = default!;

    [JsonPropertyName("likeness_score")]
    public int LikenessScore { get; set; } = default!;
}