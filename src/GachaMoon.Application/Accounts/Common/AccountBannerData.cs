using GachaMoon.Domain.Banners;

namespace GachaMoon.Application.Accounts.Common;
public record AccountBannerData
{
    public BannerType BannerType { get; init; }
    public int TotalRolls { get; init; }
    public int RollsToLegendary { get; init; }
    public int RollsToEpic { get; init; }
    public int TotalEpicRolls { get; init; }
    public int TotalLegendaryRolls { get; init; }
}