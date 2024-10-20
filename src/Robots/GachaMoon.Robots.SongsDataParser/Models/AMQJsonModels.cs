namespace GachaMoon.Robots.SongsDataParser.Models;

public class AMQJsonData
{
    public required Dictionary<long, AMQAnimeData> AnimeMap { get; set; }
}

public class AMQAnimeData
{
    public required int AnnId { get; set; }
    public string? Category { get; set; }
    public required AMQMainAnimeNames MainNames { get; set; }
}

public class AMQMainAnimeNames
{
    public required string JA { get; set; }
    public string? EN { get; set; }
}