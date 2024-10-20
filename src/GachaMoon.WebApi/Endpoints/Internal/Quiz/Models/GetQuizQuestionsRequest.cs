using GachaMoon.Application.Internal.GetQuizQuestions;

namespace GachaMoon.WebApi.Endpoints.Internal.Quiz.Models;

public record GetQuizQuestionsRequest(int QuestionNumber, bool DiversifyAnime, int MinRating, int MaxRating, int MinReleaseYear, int MaxReleaseYear,
     int ImageQuestions, int SongQuestions, bool AllowOps, bool AllowEds, bool AllowIns, bool AllowTv, bool AllowMovie, bool AllowOva, bool AllowSpecial, bool AllowMusic, ICollection<long> PlayerAccountIds)
{
    public GetQuizQuestionsQuery ToQuery()
    {
        return new GetQuizQuestionsQuery(QuestionNumber, DiversifyAnime, MinRating, MaxRating, MinReleaseYear, MaxReleaseYear, ImageQuestions, SongQuestions,
            AllowOps, AllowEds, AllowIns, AllowMovie, AllowSpecial, AllowMusic, AllowTv, AllowOva, PlayerAccountIds);
    }
}
