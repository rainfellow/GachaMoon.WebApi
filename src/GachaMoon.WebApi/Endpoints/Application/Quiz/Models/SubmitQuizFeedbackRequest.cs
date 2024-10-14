using GachaMoon.Application.Quiz.SubmitQuizFeedback;

namespace GachaMoon.WebApi.Endpoints.Application.Quiz.Models;

public record SubmitQuizFeedbackRequest(string GameTitle, ICollection<QuestionFeedbackData> FeedbackData)
{
    public SubmitQuizFeedbackCommand ToCommand(long AccountId)
    {
        return new SubmitQuizFeedbackCommand(AccountId, GameTitle, FeedbackData);
    }
}
