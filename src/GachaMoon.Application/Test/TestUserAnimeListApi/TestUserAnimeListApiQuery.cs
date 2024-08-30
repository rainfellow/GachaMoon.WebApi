namespace GachaMoon.Application.Test.TestUserAnimeListApi;

public class TestUserAnimeListApiQuery(string query) : IRequest<TestUserAnimeListApiQueryResult>
{
    public string Query { get; init; } = query;
}
