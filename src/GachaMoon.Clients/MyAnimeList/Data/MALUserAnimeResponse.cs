namespace GachaMoon.Clients.MyAnimeList.Data;
public record MALUserAnimeResponse
{
    public ICollection<MALUserAnimeData> Data { get; set; } = default!;
}