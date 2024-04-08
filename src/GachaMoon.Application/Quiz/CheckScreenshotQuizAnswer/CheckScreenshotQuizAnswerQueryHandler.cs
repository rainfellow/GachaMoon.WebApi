using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Anime;

namespace GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;

public class CheckScreenshotQuizAnswerQueryHandler(ApplicationDbContext dbContext, IAnimeScreenshotQuizService screenshotQuizService) : IRequestHandler<CheckScreenshotQuizAnswerQuery, CheckScreenshotQuizAnswerQueryResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IAnimeScreenshotQuizService _screenshotQuizService = screenshotQuizService;

    public async Task<CheckScreenshotQuizAnswerQueryResult> Handle(CheckScreenshotQuizAnswerQuery request, CancellationToken cancellationToken)
    {
        return new CheckScreenshotQuizAnswerQueryResult(await _screenshotQuizService.CheckAnswer(request.Question, request.Answer));
    }
}