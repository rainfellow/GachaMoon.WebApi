namespace GachaMoon.Services.Anime.Data;

public record CachedAnimeData
{
    public int AnilistId { get; set; }
    public int MyAnimeListId { get; set; }
    public string Title { get; set; } = default!;
    public long InternalId { get; set; } = default!;
    public string AnimeType { get; set; } = default!;
    public string AgeRating { get; set; } = default!;
    public DateOnly StartDate { get; set; } = default!;
    public double MeanScore { get; set; } = default!;
    public bool HasImages { get; set; } = default!;
    public bool HasSongs { get; set; } = default!;
    public bool HasOps { get; set; } = default!;
    public bool HasEds { get; set; } = default!;
    public bool HasIns { get; set; } = default!;
}