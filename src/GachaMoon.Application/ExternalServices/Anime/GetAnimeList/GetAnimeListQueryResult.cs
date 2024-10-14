using GachaMoon.Domain.ExternalServices;
using UserAnimeData = GachaMoon.Domain.ExternalServices.UserAnimeData;

namespace GachaMoon.Application.ExternalServices.Anime.GetAnimeList;

public record GetAnimeListQueryResult()
{
    public string AnimeListUserId { get; init; } = default!;
    public ExternalServiceProvider AnimeListServiceProvider { get; init; }
    public ICollection<string> SelectedAnimeGroups { get; init; } = default!;
    public ICollection<UserAnimeData> UserAnimes { get; init; } = default!;
}