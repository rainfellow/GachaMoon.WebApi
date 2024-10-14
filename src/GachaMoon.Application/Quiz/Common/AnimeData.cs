namespace GachaMoon.Application.Quiz.Common;

public record AnimeData
{
    public long AnimeId { get; init; }
    public string AnimeName { get; init; } = default!;
    public int MalId { get; init; }
    public int EpisodeCount { get; init; }
    public string AgeRating { get; init; } = default!;
    public string AnimeType { get; init; } = default!;
    public string ReleaseDate { get; init; } = default!;
    public double MeanScore { get; init; }
    public ICollection<AnimeAliasData> Aliases { get; init; } = default!;
};
