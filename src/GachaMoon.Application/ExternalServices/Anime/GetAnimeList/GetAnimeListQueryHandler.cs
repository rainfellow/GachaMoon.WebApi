using GachaMoon.Services.Abstractions.Anime;

namespace GachaMoon.Application.ExternalServices.Anime.GetAnimeList;

public class GetAnimeListQueryHandler(IUserSavedAnimeListService animeListService) : IRequestHandler<GetAnimeListQuery, GetAnimeListQueryResult>
{
    private readonly IUserSavedAnimeListService _animeListService = animeListService;

    public async Task<GetAnimeListQueryResult> Handle(GetAnimeListQuery request, CancellationToken cancellationToken)
    {
        var result = await _animeListService.GetAnimeList(request.AccountId, cancellationToken);
        return new GetAnimeListQueryResult
        {
            AnimeListServiceProvider = result.AnimeListServiceProvider,
            UserAnimes = result.UserAnimes,
            SelectedAnimeGroups = result.SelectedAnimeGroups,
            AnimeListUserId = result.AnimeListUserId
        };
    }
}