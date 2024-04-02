using GachaMoon.Application.Banners.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Banners.List;

public class ListBannersQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<ListBannersQuery, ListBannersQueryResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ListBannersQueryResult> Handle(ListBannersQuery request, CancellationToken cancellationToken)
    {
        // Handle the query and return the result
        // ...
        var currentBanners = await _dbContext.Banners
            .IsNotDeleted()
            .Select(x => new { x.Type, x.Id, x.Title, BannerCharacters = x.BannerCharacters.Where(x => x.DeletedAt == null) })
            .ToListAsync(cancellationToken);

        return new ListBannersQueryResult(currentBanners.Select(x => new BannerData
        {
            BannerTitle = x.Title,
            BannerType = x.Type,
            CharacterIds = x.BannerCharacters.Select(x => x.CharacterId).ToList()
        }).ToList());
    }
}