namespace GachaMoon.Application.Test.TestAnimeApi;

public class TestAnimeApiQuery(string query) : IRequest<TestAnimeApiQueryResult>
{
    public string Query { get; init; } = query;
}
