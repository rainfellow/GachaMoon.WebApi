
namespace GachaMoon.Clients.Anilist.Data;

public class AnilistList
{
    public required string Name { get; set; }
    public required List<AnilistEntry> Entries { get; set; }
}

public class AnilistEntry
{
    public required AnilistMedia Media { get; set; }
}

public class AnilistMedia
{
    public required int Id { get; set; }
    public required AnilistTitle Title { get; set; }
}

public class AnilistTitle
{
    public required string Romaji { get; set; }
}

public class MediaListCollection
{
    public required List<AnilistList> Lists { get; set; }
}

public class AnilistUserListResponse
{
    public required MediaListCollection MediaListCollection { get; set; }
}