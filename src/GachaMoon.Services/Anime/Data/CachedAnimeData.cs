namespace GachaMoon.Services.Anime.Data;

public record CachedAnimeData
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public long InternalId { get; set; } = default!;
    public string AnimeType { get; set; } = default!;
    public string AgeRating { get; set; } = default!;
    public DateOnly StartDate { get; set; } = default!;
    public double MeanScore { get; set; } = default!;
}