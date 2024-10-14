using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Quiz;

public class GameFeedback : SoftDeleteEntityBase<long>
{
    public Feedback Feedback { get; set; } = default!;

    public long AccountId { get; set; } = default!;
    public virtual Account Account { get; set; } = default!;

    public long GameResultId { get; set; } = default!;
    public virtual GameResult GameResult { get; set; } = default!;
}
public class Feedback
{
    public ICollection<QuestionFeedback> QuestionsFeedback { get; set; } = default!;
}

public class QuestionFeedback
{
    public DifficultyFeedback DifficultyFeedback { get; set; }
    public PlayabilityFeedback PlayabilityFeedback { get; set; }
}

public enum DifficultyFeedback
{
    None,
    Obvious,
    Easy,
    Normal,
    Hard,
    Impossible
}

public enum PlayabilityFeedback
{
    None,
    Unplayable,
    Normal,
    Good
}