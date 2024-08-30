// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Animes;

public class AnimeImage : SoftDeleteEntityBase<long>
{
    public string Url { get; set; } = default!;
    public long VoteSum { get; set; } = default!;
    public int VoteCount { get; set; } = default!;
    public int BadVoteCount { get; set; } = default!;

    public long AnimeEpisodeId { get; set; }
    public AnimeEpisode AnimeEpisode { get; set; } = default!;
}