using GachaMoon.Application.Accounts.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Accounts.ListAccountCharacters;

public class ListAccountCharactersQueryHandler : IRequestHandler<ListAccountCharactersQuery, ListAccountCharactersQueryResult>
{
    private readonly ApplicationDbContext _dbContext;

    public ListAccountCharactersQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListAccountCharactersQueryResult> Handle(ListAccountCharactersQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .IsNotDeleted()
            .Select(x => new
            {
                x.Id,
                x.AccountName
            })
            .FirstOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken)
            ?? throw new NotImplementedException($"Account with id {request.AccountId} not found.");

        var accountCharacters = await _dbContext.AccountCharacters
            .IsNotDeleted()
            .Where(x => x.AccountId == request.AccountId)
            .Select(x => new
            {
                Id = x.Id,
                CharacterId = x.CharacterId,
                RepeatCount = x.RepeatCount,
                CharacterLevel = x.CharacterLevel,
                CharacterExperience = x.CharacterExperience,
            })
            .ToListAsync(cancellationToken);

        var charIds = accountCharacters.Select(c => c.Id);

        var charSkills = await _dbContext.AccountCharacterAbilities
            .IsNotDeleted()
            .Where(x => charIds.Contains(x.AccountCharacterId))
            .Select(x => new
            {
                x.CharacterAbilityId,
                x.AccountCharacterId,
                x.AbilityType
            })
            .ToListAsync(cancellationToken);

        var skillsGrouped = charSkills.GroupBy(x => x.AccountCharacterId);

        var result = accountCharacters.Join(skillsGrouped, x => x.Id, f => f.Key, (x, f) => new AccountCharacterData
        {
            CharacterId = x.CharacterId,
            RepeatCount = x.RepeatCount,
            CharacterLevel = x.CharacterLevel,
            CharacterExperience = x.CharacterExperience,
            Abilities = f.Select(e => new AccountCharacterAbilityData
            {
                CharacterAbilityId = e.CharacterAbilityId,
                AbilityType = e.AbilityType
            }).ToList()
        }).ToList();

        return new()
        {
            AccountName = account.AccountName,
            AccountCharacters = result
        };
    }
}
