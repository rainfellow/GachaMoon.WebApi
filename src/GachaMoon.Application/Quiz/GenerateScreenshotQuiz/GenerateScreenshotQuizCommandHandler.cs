using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Anime;

namespace GachaMoon.Application.Quiz.GenerateScreenshotQuiz;

public class GenerateScreenshotQuizCommandHandler(ApplicationDbContext dbContext, IAnimeScreenshotQuizService screenshotQuizService) : IRequestHandler<GenerateScreenshotQuizCommand, GenerateScreenshotQuizCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IAnimeScreenshotQuizService _screenshotQuizService = screenshotQuizService;

    public async Task<GenerateScreenshotQuizCommandResult> Handle(GenerateScreenshotQuizCommand request, CancellationToken cancellationToken)
    {
        return new GenerateScreenshotQuizCommandResult(await _screenshotQuizService.GenerateQuestion());
    }
}