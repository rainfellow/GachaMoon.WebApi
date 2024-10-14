namespace GachaMoon.Domain.Quiz;

public class GameRecap
{
    public ICollection<QuestionRecap> CorrectAnswers { get; set; } = default!;
    public Dictionary<long, ICollection<PlayerAnswerRecap>> PlayerAnswersRecaps { get; set; } = default!;

}

public class PlayerAnswerRecap
{
    public long PlayerAnswer { get; init; }
    public bool IsCorrect { get; init; }
    public double? TimeToAnswer { get; init; }
    public int FromEpisode { get; init; }
}

public class QuestionRecap
{
    public long Answer { get; init; }
    public string Question { get; init; } = default!;
}