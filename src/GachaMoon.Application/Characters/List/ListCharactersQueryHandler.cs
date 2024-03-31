using GachaMoon.Application.Characters.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Characters.List;

public class ListCharactersQueryHandler : IRequestHandler<ListCharactersQuery, ListCharactersQueryResult>
{
    private readonly ApplicationDbContext _dbContext;

    public ListCharactersQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListCharactersQueryResult> Handle(ListCharactersQuery request, CancellationToken cancellationToken)
    {
        var chars = await _dbContext.Characters
            .IsNotDeleted()
            .Select(x => new
            {
                Id = x.Id,
                x.CharacterType,
                x.Rarity,
                x.Name
            })
            .ToListAsync(cancellationToken);

        var charIds = chars.Select(c => c.Id);

        var charBaseStats = await _dbContext.CharacterBaseStats
            .IsNotDeleted()
            .Where(x => charIds.Contains(x.CharacterId) && x.CharacterLevel == 1)
            .Select(x => new
            {
                x.CharacterId,
                x.Health,
                x.Defence,
                x.Attack
            })
            .ToListAsync(cancellationToken);


        var charSkills = await _dbContext.DefaultCharacterAbilities
            .IsNotDeleted()
            .Where(x => charIds.Contains(x.CharacterId))
            .Select(x => new
            {
                x.CharacterAbilityId,
                x.CharacterId,
                x.AbilityType
            })
            .ToListAsync(cancellationToken);

        var skillsGrouped = charSkills.GroupBy(x => x.CharacterId);

        var charsWithStats = chars.Join(charBaseStats, x => x.Id, f => f.CharacterId, (x, f) => new
        {
            x.Id,
            x.Name,
            x.Rarity,
            x.CharacterType,
            Health = f.Health,
            Defence = f.Defence,
            Attack = f.Attack
        });

        var result = charsWithStats.Join(skillsGrouped, x => x.Id, f => f.Key, (x, f) => new CharacterData
        {
            CharacterId = x.Id,
            Name = x.Name,
            Rarity = x.Rarity,
            Type = x.CharacterType,
            BaseHealth = x.Health,
            BaseAttack = x.Attack,
            BaseDefence = x.Defence,
            Abilities = f.Select(e => new CharacterDefaultAbilityData
            {
                CharacterAbilityId = e.CharacterAbilityId,
                AbilityType = e.AbilityType
            }).ToList()
        }).ToList();

        return new ListCharactersQueryResult(result);
    }
}
