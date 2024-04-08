namespace GachaMoon.Services.Anime.Data;
public record JsonAnimeImageData
{
    public string Url { get; set; } = default!;
    public int Difficulty { get; set; } = default!;
}