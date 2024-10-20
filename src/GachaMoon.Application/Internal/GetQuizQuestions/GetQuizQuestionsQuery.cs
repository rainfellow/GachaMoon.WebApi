namespace GachaMoon.Application.Internal.GetQuizQuestions;

public class GetQuizQuestionsQuery(int questionNumber, bool diversifyAnime, int minRating, int maxRating, int minReleaseYear, int maxReleaseYear,
    int imageQuestions, int songQuestions, bool allowOps, bool allowEds, bool allowIns, bool allowMovie, bool allowSpecial, bool allowMusic, bool allowTv, bool allowOva, ICollection<long> playerAccountIds)
 : IRequest<GetQuizQuestionsQueryResult>
{
    public int QuestionNumber { get; init; } = questionNumber;
    public bool DiversifyAnime { get; init; } = diversifyAnime;
    public int MinRating { get; init; } = minRating;
    public int MaxRating { get; init; } = maxRating;
    public int MinReleaseYear { get; init; } = minReleaseYear;
    public int MaxReleaseYear { get; init; } = maxReleaseYear;
    public int ImageQuestions { get; init; } = imageQuestions;
    public int SongQuestions { get; init; } = songQuestions;
    public bool AllowOps { get; init; } = allowOps;
    public bool AllowEds { get; init; } = allowEds;
    public bool AllowIns { get; init; } = allowIns;
    public bool AllowMovie { get; init; } = allowMovie;
    public bool AllowSpecial { get; init; } = allowSpecial;
    public bool AllowMusic { get; init; } = allowMusic;
    public bool AllowTv { get; init; } = allowTv;
    public bool AllowOva { get; init; } = allowOva;
    public ICollection<long> PlayerAccountIds { get; init; } = playerAccountIds;
}
