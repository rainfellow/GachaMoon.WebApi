using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.Quiz;
using GachaMoon.Services.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Quiz.SubmitQuizFeedback;

public class SubmitQuizFeedbackCommandHandler(ApplicationDbContext applicationDbContext, IClockProvider clockProvider) : IRequestHandler<SubmitQuizFeedbackCommand>
{
    private readonly ApplicationDbContext _dbContext = applicationDbContext;
    private readonly IClockProvider _clockProvider = clockProvider;

    public async Task Handle(SubmitQuizFeedbackCommand request, CancellationToken cancellationToken)
    {
        var now = _clockProvider.Now;
        var game = await _dbContext.GameResults
            .IsNotDeleted()
            .Where(x => x.Title == request.GameTitle)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotImplementedException();

        var feedback = new Feedback
        {
            QuestionsFeedback = request.FeedbackData.Select(x => new QuestionFeedback
            {
                DifficultyFeedback = x.DifficultyFeedback,
                PlayabilityFeedback = x.PlayabilityFeedback
            }).ToList()
        };

        var gameFeedback = new GameFeedback
        {
            AccountId = request.AccountId,
            GameResultId = game.Id,
            Feedback = feedback,
            CreatedAt = now,
            UpdatedAt = now
        };
        await _dbContext.GameFeedbacks.AddAsync(gameFeedback, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
