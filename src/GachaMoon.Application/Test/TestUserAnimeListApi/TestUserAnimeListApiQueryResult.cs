namespace GachaMoon.Application.Test.TestUserAnimeListApi;

public class TestUserAnimeListApiQueryResult(int id, string name)
{
    public int Id { get; init; } = id;
    public string Name { get; init; } = name;
}