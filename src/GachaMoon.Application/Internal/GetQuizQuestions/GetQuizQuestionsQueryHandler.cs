using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Abstractions.Anime.Data;

namespace GachaMoon.Application.Internal.GetQuizQuestions;

public class GetQuizQuestionsQueryHandler(IAnimeScreenshotQuizService animeScreenshotQuizService, IUserSavedAnimeListService animeListService) : IRequestHandler<GetQuizQuestionsQuery, GetQuizQuestionsQueryResult>
{
    private readonly IAnimeScreenshotQuizService _animeScreenshotQuizService = animeScreenshotQuizService;
    private readonly IUserSavedAnimeListService _animeListService = animeListService;

    public async Task<GetQuizQuestionsQueryResult> Handle(GetQuizQuestionsQuery request, CancellationToken cancellationToken)
    {
        var animeLists = await _animeListService.MassGetAnimeLists(request.PlayerAccountIds, cancellationToken);

        var generatedQuiz = await _animeScreenshotQuizService.GenerateQuiz(new QuizConfigData
        {
            QuestionNumber = request.QuestionNumber,
            DiversifyAnime = request.DiversifyAnime,
            MinRating = request.MinRating,
            MaxRating = request.MaxRating,
            MinReleaseYear = request.MinReleaseYear,
            MaxReleaseYear = request.MaxReleaseYear
        }, animeLists);

        return new()
        {
            QuizQuestions = generatedQuiz
        };
    }
}
