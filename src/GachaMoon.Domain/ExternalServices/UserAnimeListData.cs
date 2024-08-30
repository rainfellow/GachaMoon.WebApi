namespace GachaMoon.Domain.ExternalServices;
public class UserAnimeListData
{
    public ICollection<UserAnimeData> UserAnimes { get; set; } = default!;
}