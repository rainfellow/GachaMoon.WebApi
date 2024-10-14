namespace GachaMoon.Services.Abstractions.Clients.Data;
public record AnimeData
{
    public string Title { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public ICollection<string> Synonyms { get; set; } = default!;
    public ICollection<string> Sources { get; set; } = default!;
    public ProviderMapping ProviderMapping { get; set; } = default!;
}

public class ProviderMapping
{
    public string AniDB { get; set; } = default!;
    public string MyAnimeList { get; set; } = default!;
    public string Anilist { get; set; } = default!;
}