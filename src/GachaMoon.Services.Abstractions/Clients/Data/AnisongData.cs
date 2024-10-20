using System.Text.Json.Serialization;

namespace GachaMoon.Services.Abstractions.Clients.Data;

public record AnisongSongData
{
    [JsonPropertyName("linked_ids")]
    public LinkedIds LinkedIds { get; set; } = default!;
    public string? Audio { get; set; }
    public string? MQ { get; set; }
    public string? HQ { get; set; }
    public double? SongDifficulty { get; set; }
    public string? SongCategory { get; set; }
    public string SongName { get; set; } = default!;
    public string SongType { get; set; } = default!;

    public ICollection<AnisongArtistData> Artists { get; set; } = default!;
    public ICollection<AnisongArtistData> Composers { get; set; } = default!;
    public ICollection<AnisongArtistData> Arrangers { get; set; } = default!;
}

public record LinkedIds
{
    public int? Myanimelist { get; set; }
    public int? Anilist { get; set; }
}

public record AnisongArtistData
{
    public long Id { get; set; }
    public ICollection<string> Names { get; set; } = default!;
}