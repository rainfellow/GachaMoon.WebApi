namespace GachaMoon.Services.Anime.Data;
public record JsonAnimeData
{
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public ICollection<JsonAnimeEpisodeData> Episodes { get; set; } = default!;
}