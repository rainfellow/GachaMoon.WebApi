// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Animes;

public class AnimeSong : SoftDeleteEntityBase<long>
{
    public string? SongTypeString { get; set; } = default!;
    public string? SongArtist { get; set; } = default!;
    public double AMQSongDifficulty { get; set; } = default!;
    public string AMQSongCategory { get; set; } = default!;
    public string? CatboxAudioLink { get; set; } = default!;
    public string? CatboxMQLink { get; set; } = default!;
    public string? CatboxHQLink { get; set; } = default!;
    public string? OpeningsMoeLink { get; set; } = default!;
    public string? GachamoonLink { get; set; } = default!;
    public SongType SongType { get; set; } = default!;
    public string SongName { get; set; } = default!;

    public long AnimeId { get; set; }
    public Anime Anime { get; set; } = default!;

    public virtual ICollection<AnimeSongArtist> AnimeSongArtists { get; set; } = new HashSet<AnimeSongArtist>();
}

public enum SongType
{
    None,
    Opening,
    Ending,
    Insert
}