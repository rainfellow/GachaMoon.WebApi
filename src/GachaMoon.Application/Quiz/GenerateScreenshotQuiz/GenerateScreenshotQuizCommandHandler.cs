using GachaMoon.Services.Abstractions.Anime;

namespace GachaMoon.Application.Quiz.GenerateScreenshotQuiz;

public class GenerateScreenshotQuizCommandHandler(IAnimeScreenshotQuizService screenshotQuizService, IUserSavedAnimeListService animeListService) : IRequestHandler<GenerateScreenshotQuizCommand, GenerateScreenshotQuizCommandResult>
{
    private readonly IAnimeScreenshotQuizService _screenshotQuizService = screenshotQuizService;
    private readonly IUserSavedAnimeListService _animeListService = animeListService;

    public async Task<GenerateScreenshotQuizCommandResult> Handle(GenerateScreenshotQuizCommand request, CancellationToken cancellationToken)
    {
        if (request.UseConnectedAccount)
        {
            var userAnimeList = await _animeListService.GetAnimeList(request.AccountId, cancellationToken);
            return new GenerateScreenshotQuizCommandResult(await _screenshotQuizService.GenerateQuestion(userAnimeList));
        }
        else
        {
            return new GenerateScreenshotQuizCommandResult(await _screenshotQuizService.GenerateQuestion());
        }
    }
}