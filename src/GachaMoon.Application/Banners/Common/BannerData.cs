using GachaMoon.Domain.Banners;

namespace GachaMoon.Application.Banners.Common;

public record BannerData
{
    public string BannerTitle { get; init; } = default!;
    public BannerType BannerType { get; init; } = default!;
    public ICollection<long> CharacterIds { get; init; } = default!;
};
