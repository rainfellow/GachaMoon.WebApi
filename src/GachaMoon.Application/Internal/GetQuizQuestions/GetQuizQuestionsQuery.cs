namespace GachaMoon.Application.Internal.GetQuizQuestions;

public class GetQuizQuestionsQuery(int questionNumber, bool diversifyAnime, int minRating, int maxRating, int minReleaseYear, int maxReleaseYear, ICollection<long> playerAccountIds)
 : IRequest<GetQuizQuestionsQueryResult>
{
    public int QuestionNumber { get; init; } = questionNumber;
    public bool DiversifyAnime { get; init; } = diversifyAnime;
    public int MinRating { get; init; } = minRating;
    public int MaxRating { get; init; } = maxRating;
    public int MinReleaseYear { get; init; } = minReleaseYear;
    public int MaxReleaseYear { get; init; } = maxReleaseYear;
    public ICollection<long> PlayerAccountIds { get; init; } = playerAccountIds;
}
