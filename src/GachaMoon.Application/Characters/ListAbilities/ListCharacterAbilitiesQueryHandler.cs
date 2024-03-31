using GachaMoon.Application.Characters.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Characters.ListAbilities;

public class ListCharacterAbilitiesQueryHandler : IRequestHandler<ListCharacterAbilitiesQuery, ListCharacterAbilitiesQueryResult>
{
    private readonly ApplicationDbContext _dbContext;

    public ListCharacterAbilitiesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListCharacterAbilitiesQueryResult> Handle(ListCharacterAbilitiesQuery request, CancellationToken cancellationToken)
    {
        var abilities = await _dbContext.CharacterAbilities
            .IsNotDeleted()
            .Select(x => new CharacterAbilityData
            {
                AbilityId = x.Id,
                AbilityType = x.AbilityType,
                AbilityTarget = x.AbilityTarget,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync(cancellationToken);

        return new ListCharacterAbilitiesQueryResult(abilities);
    }
}
