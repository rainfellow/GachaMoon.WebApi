namespace GachaMoon.Clients.AnimeList.Data;
public record MALAnimeStatus
{
    public string Status { get; set; } = default!;
    public int Score { get; set; } = default!;

}