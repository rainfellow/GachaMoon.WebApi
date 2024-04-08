namespace GachaMoon.Clients.AnimeList.Data;
public record MALUserAnimeData
{
    public MALAnimeNode Node { get; set; } = default!;
}