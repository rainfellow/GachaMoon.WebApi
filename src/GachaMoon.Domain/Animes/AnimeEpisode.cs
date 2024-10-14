// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Animes;

public class AnimeEpisode : SoftDeleteEntityBase<long>
{
    public string Title { get; set; } = default!;
    public long AnimeId { get; set; } = default!;
    public int EpisodeNumber { get; set; } = default!;

    public virtual Anime Anime { get; set; } = default!;
    public virtual ICollection<AnimeImage> AnimeImages { get; set; } = new HashSet<AnimeImage>();
}