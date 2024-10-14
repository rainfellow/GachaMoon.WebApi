using GachaMoon.Database;
using GachaMoon.Domain.Quiz;
using GachaMoon.Services.Abstractions.Time;

namespace GachaMoon.Application.Internal.SubmitQuizResult;

public class SubmitQuizResultCommandHandler(ApplicationDbContext dbContext, IClockProvider clockProvider) : IRequestHandler<SubmitQuizResultCommand>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IClockProvider _clockProvider = clockProvider;

    public async Task Handle(SubmitQuizResultCommand request, CancellationToken cancellationToken)
    {
        var now = _clockProvider.Now;
        var newResult = new GameResult
        {
            Title = request.GameTitle,
            GameRecap = request.GameRecap,
            GameType = request.GameType,
            CreatedAt = now,
            UpdatedAt = now
        };
        await _dbContext.GameResults.AddAsync(newResult, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
