using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace GachaMoon.Domain.Animes;

public class AnimeSongArtist
{
    [Key]
    public long ArtistId { get; set; }
    public string ArtistName { get; set; } = default!;
    public AnimeSongArtistType ArtistType { get; set; } = default!;
    public Instant? CreatedAt { get; set; }
    public Instant? UpdatedAt { get; set; }
    public Instant? DeletedAt { get; set; }

    public virtual ICollection<AnimeSong> AnimeSongs { get; set; } = new HashSet<AnimeSong>();
}

public enum AnimeSongArtistType
{
    None,
    Performer,
    Composer,
    Arranger
}