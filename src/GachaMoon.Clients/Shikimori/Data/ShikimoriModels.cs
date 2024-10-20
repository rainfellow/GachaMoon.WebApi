
namespace GachaMoon.Clients.Shikimori.Data;

public class ShikimoriUser
{
    public required long Id { get; set; }
    public required string Nickname { get; set; }
}

public class ShikimoriUserResponse
{
    public required List<ShikimoriUser> Users { get; set; }
}

public class ShikimoriAnime
{
    public required int Id { get; set; }
    public required string Russian { get; set; }
}

public class ShikimoriUserAnime
{
    public required ShikimoriAnime Anime { get; set; }
    public required string Status { get; set; }
}

public class ShikimoriUserListResponse
{
    public required List<ShikimoriUserAnime> UserRates { get; set; }
}

public class ShikimorAnimeDetailsResponse
{
    public required List<ShikimoriAnimeData> Animes { get; set; }
}