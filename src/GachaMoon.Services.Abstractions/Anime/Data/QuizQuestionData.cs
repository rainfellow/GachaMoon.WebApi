using System.Text.Json.Serialization;

namespace GachaMoon.Services.Abstractions.Anime.Data;
public record QuizQuestionData
{
    public string Question { get; set; } = default!;
    public long Answer { get; set; } = default!;
    public int FromEpisode { get; set; } = default!;
    public QuizQuestionType QuestionType { get; set; } = default!;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum QuizQuestionType
{
    Image = 0,
    Song = 1
}