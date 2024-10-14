using GachaMoon.Application.Internal.GetQuizQuestions;

namespace GachaMoon.WebApi.Endpoints.Internal.Quiz.Models;

public record GetQuizQuestionsRequest(int QuestionNumber, bool DiversifyAnime, int MinRating, int MaxRating, int MinReleaseYear, int MaxReleaseYear, ICollection<long> PlayerAccountIds)
{
    public GetQuizQuestionsQuery ToQuery()
    {
        return new GetQuizQuestionsQuery(QuestionNumber, DiversifyAnime, MinRating, MaxRating, MinReleaseYear, MaxReleaseYear, PlayerAccountIds);
    }
}
