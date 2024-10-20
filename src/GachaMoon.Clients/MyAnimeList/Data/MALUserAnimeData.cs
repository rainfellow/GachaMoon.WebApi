namespace GachaMoon.Clients.MyAnimeList.Data;
public record MALUserAnimeData
{
    public MALAnimeNode Node { get; set; } = default!;
}