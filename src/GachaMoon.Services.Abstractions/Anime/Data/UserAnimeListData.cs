using GachaMoon.Domain.ExternalServices;

namespace GachaMoon.Services.Abstractions.Anime.Data;
public record UserAnimeListData
{
    public string AnimeListUserId { get; set; } = default!;
    public ExternalServiceProvider AnimeListServiceProvider { get; set; } = ExternalServiceProvider.None;
    public ICollection<UserAnimeData> UserAnimes { get; set; } = new List<UserAnimeData>();
    public ICollection<string> SelectedAnimeGroups { get; set; } = new List<string>();
}