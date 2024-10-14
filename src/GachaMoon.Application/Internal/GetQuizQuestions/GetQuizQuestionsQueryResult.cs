using GachaMoon.Services.Abstractions.Anime.Data;

namespace GachaMoon.Application.Internal.GetQuizQuestions;

public record GetQuizQuestionsQueryResult
{
    public required ICollection<QuizQuestionData> QuizQuestions { get; init; }
}
