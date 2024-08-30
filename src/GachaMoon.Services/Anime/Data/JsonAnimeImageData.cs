namespace GachaMoon.Services.Anime.Data;
public record JsonAnimeImageData
{
    public string Url { get; set; } = default!;
    public int VoteCount { get; set; } = default!;
    public int VoteSum { get; set; } = default!;
    public int BadVoteCount { get; set; } = default!;
}