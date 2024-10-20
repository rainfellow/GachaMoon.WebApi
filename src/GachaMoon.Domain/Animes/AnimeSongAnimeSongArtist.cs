using GachaMoon.Domain.Animes;
using GachaMoon.Domain.Base;
public class AnimeSongAnimeSongArtist : SoftDeleteEntityBase<long>
{
    public long AnimeSongId { get; set; }
    public long ArtistId { get; set; }

    public virtual AnimeSong AnimeSong { get; set; } = default!;
    public virtual AnimeSongArtist Artist { get; set; } = default!;
}
