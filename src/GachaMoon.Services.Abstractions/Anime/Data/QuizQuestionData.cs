namespace GachaMoon.Services.Abstractions.Anime.Data;
public record QuizQuestionData
{
    public string Question { get; set; } = default!;
    public long Answer { get; set; } = default!;
    public int FromEpisode { get; set; } = default!;
}