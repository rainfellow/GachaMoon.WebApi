// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Animes;

public class Anime : SoftDeleteEntityBase<long>
{
    public string Title { get; set; } = default!;
    public int AnimeBaseId { get; set; } = default!;
    public int AnilistId { get; set; } = default!;
    public string ImageSiteTitle { get; set; } = default!;
    public int EpisodeCount { get; set; } = default!;
    public string AnimeType { get; set; } = default!;
    public string AgeRating { get; set; } = default!;
    public string StartDate { get; set; } = default!;
    public double MeanScore { get; set; } = default!;

    public virtual ICollection<AnimeEpisode> AnimeEpisodes { get; set; } = new HashSet<AnimeEpisode>();

    public virtual ICollection<AnimeAlias> AnimeAliases { get; set; } = new HashSet<AnimeAlias>();
}