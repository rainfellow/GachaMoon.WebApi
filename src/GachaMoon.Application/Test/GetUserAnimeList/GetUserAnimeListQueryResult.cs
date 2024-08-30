using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Application.Test.GetUserAnimeList;

public record GetUserAnimeListQueryResult(ICollection<UserAnimeData> UserAnime)
{
}