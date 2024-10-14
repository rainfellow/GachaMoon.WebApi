using GachaMoon.Application.Quiz.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Quiz.ListAllAnimes;

public class ListAllAnimesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<ListAllAnimesQuery, ListAllAnimesQueryResult>
{
    private readonly ApplicationDbContext _dbContext = applicationDbContext;

    public async Task<ListAllAnimesQueryResult> Handle(ListAllAnimesQuery request, CancellationToken cancellationToken)
    {
        var animes = await _dbContext.Animes
            .IsNotDeleted()
            .Include(x => x.AnimeAliases)
            .Select(x => new
            {
                AnimeId = x.Id,
                AnimeName = x.Title,
                MALId = x.AnimeBaseId,
                MeanScore = x.MeanScore,
                AgeRating = x.AgeRating,
                AnimeType = x.AnimeType,
                EpisodeCount = x.EpisodeCount,
                StartDate = x.StartDate,
                Aliases = x.AnimeAliases.Where(x => x.DeletedAt == null).Select(e => new AnimeAliasData(e.AnimeId, e.Alias, e.Language, e.Id))
            })
            .ToListAsync();

        return new()
        {
            AnimeData = animes.Select(x => new AnimeData
            {
                AnimeId = x.AnimeId,
                AnimeName = x.AnimeName,
                MalId = x.MALId,
                MeanScore = x.MeanScore,
                AgeRating = x.AgeRating,
                AnimeType = x.AnimeType,
                EpisodeCount = x.EpisodeCount,
                ReleaseDate = x.StartDate,
                Aliases = x.Aliases.ToList()
            }).ToList()
        };
    }
}
