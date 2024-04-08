namespace GachaMoon.Services.Anime.Data;
public record JsonAnimeEpisodeData
{
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public ICollection<JsonAnimeImageData> Images { get; set; } = default!;
}