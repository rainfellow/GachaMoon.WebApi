namespace GachaMoon.Domain.ExternalServices;
public class UserAnimeListData
{
    public ICollection<UserAnimeData> UserAnimes { get; set; } = default!;
    public ICollection<string> UserAnimeGroups { get; set; } = default!;
}