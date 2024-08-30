namespace GachaMoon.Application.Test.GetUserAnimeList;

public class GetUserAnimeListQuery(string userName) : IRequest<GetUserAnimeListQueryResult>
{
    public string UserName { get; init; } = userName;
}
