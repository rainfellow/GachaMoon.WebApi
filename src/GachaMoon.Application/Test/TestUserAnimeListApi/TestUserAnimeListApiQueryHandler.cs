using GachaMoon.Services.Abstractions.Clients;

namespace GachaMoon.Application.Test.TestUserAnimeListApi;

public class TestUserAnimeListApiQueryHandler(IMyAnimeListApiClient animeListClient) : IRequestHandler<TestUserAnimeListApiQuery, TestUserAnimeListApiQueryResult>
{
    private readonly IMyAnimeListApiClient _animeListClient = animeListClient;

    public async Task<TestUserAnimeListApiQueryResult> Handle(TestUserAnimeListApiQuery request, CancellationToken cancellationToken)
    {
        var result = await _animeListClient.GetAnime(request.Query, cancellationToken);
        return new TestUserAnimeListApiQueryResult(result.Id, result.Title);
    }
}