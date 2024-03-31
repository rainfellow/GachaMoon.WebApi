using GachaMoon.Application.Banners.Common;

namespace GachaMoon.Application.Banners.List;

public class ListBannersQueryResult
{
    public ICollection<BannerData> BannerDataCollection { get; init; }

    public ListBannersQueryResult(ICollection<BannerData> bannerDataCollection)
    {
        BannerDataCollection = bannerDataCollection;
    }
}