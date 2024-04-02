using GachaMoon.Application.Banners.Common;

namespace GachaMoon.Application.Banners.List;

public class ListBannersQueryResult(ICollection<BannerData> bannerDataCollection)
{
    public ICollection<BannerData> BannerDataCollection { get; init; } = bannerDataCollection;
}