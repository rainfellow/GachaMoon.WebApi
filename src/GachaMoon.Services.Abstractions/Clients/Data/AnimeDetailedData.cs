namespace GachaMoon.Services.Abstractions.Clients.Data;
public record AnimeDetailedData
{
    public int MalId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string StartDate { get; set; } = default!;
    public double MeanScore { get; set; } = default!;
    public int EpisodeCount { get; set; } = default!;
    public string AgeRating { get; set; } = default!;
    public string AnimeType { get; set; } = default!;
}