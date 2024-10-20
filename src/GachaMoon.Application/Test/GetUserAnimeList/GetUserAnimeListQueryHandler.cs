using GachaMoon.Services.Abstractions.Clients;

namespace GachaMoon.Application.Test.GetUserAnimeList;

public class GetUserAnimeListQueryHandler(IMyAnimeListApiClient animeListClient) : IRequestHandler<GetUserAnimeListQuery, GetUserAnimeListQueryResult>
{
    private readonly IMyAnimeListApiClient _animeListClient = animeListClient;

    public async Task<GetUserAnimeListQueryResult> Handle(GetUserAnimeListQuery request, CancellationToken cancellationToken)
    {
        var result = await _animeListClient.GetUserAnimeList(request.UserName, null, cancellationToken);
        return new GetUserAnimeListQueryResult(result);
    }
}