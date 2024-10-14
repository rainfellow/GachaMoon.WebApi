using GachaMoon.Common.Contracts;

namespace GachaMoon.Application.Quiz.SubmitQuizFeedback;

public class SubmitQuizFeedbackCommand(long accountId, string gameTitle, ICollection<QuestionFeedbackData> feedbackData) : IRequest, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public string GameTitle { get; init; } = gameTitle;
    public ICollection<QuestionFeedbackData> FeedbackData { get; init; } = feedbackData;
}
