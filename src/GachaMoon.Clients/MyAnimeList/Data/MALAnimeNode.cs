using System.Text.Json.Serialization;

namespace GachaMoon.Clients.MyAnimeList.Data;
public record MALAnimeNode
{
    public int Id { get; set; } = default!;
    public string Title { get; set; } = default!;

    [JsonPropertyName("start_date")]
    public string StartDate { get; set; } = default!;
    public double Mean { get; set; } = default!;

    [JsonPropertyName("num_episodes")]
    public int NumEpisodes { get; set; } = default!;

    [JsonPropertyName("media_type")]
    public string MediaType { get; set; } = default!;
    public string Rating { get; set; } = default!;
    public List<string> AlternativeTitles { get; set; } = default!;
}