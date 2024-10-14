// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Animes;

public class AnimeAlias : SoftDeleteEntityBase<long>
{
    public string Alias { get; set; } = default!;
    public long AnimeId { get; set; } = default!;
    public string Language { get; set; } = default!;

    public virtual Anime Anime { get; set; } = default!;
}