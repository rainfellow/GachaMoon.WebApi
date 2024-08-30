using GachaMoon.Services.Abstractions.Clients;

namespace GachaMoon.Application.Test.TestAnimeApi;

public class TestAnimeApiQueryHandler(IAnimeClient animeClient) : IRequestHandler<TestAnimeApiQuery, TestAnimeApiQueryResult>
{
    private readonly IAnimeClient _animeClient = animeClient;

    public async Task<TestAnimeApiQueryResult> Handle(TestAnimeApiQuery request, CancellationToken cancellationToken)
    {
        var result = await _animeClient.AnimeFromQuery(request.Query, cancellationToken);
        return new TestAnimeApiQueryResult(result.Any());
    }
}