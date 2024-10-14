namespace GachaMoon.Services.Abstractions.Anime.Data;

public record QuizConfigData
{
    public int QuestionNumber { get; set; }
    public bool DiversifyAnime { get; set; }
    public int MinRating { get; set; }
    public int MaxRating { get; set; }
    public int MinReleaseYear { get; set; }
    public int MaxReleaseYear { get; set; }

}