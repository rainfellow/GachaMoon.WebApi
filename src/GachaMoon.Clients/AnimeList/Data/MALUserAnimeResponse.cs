namespace GachaMoon.Clients.AnimeList.Data;
public record MALUserAnimeResponse
{
    public ICollection<MALUserAnimeData> Data { get; set; } = default!;
}