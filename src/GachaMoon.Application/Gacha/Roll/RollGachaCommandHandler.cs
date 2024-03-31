using GachaMoon.Application.Gacha.Common;
using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Characters;
using GachaMoon.Utilities.Constants;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Gacha.Roll;
public class RollGachaCommandHandler : IRequestHandler<RollGachaCommand, RollGachaCommandResult>
{
    private readonly ApplicationDbContext _dbContext;

    public RollGachaCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<RollGachaCommandResult> Handle(RollGachaCommand request, CancellationToken cancellationToken)
    {
        // Handle the command and return the result
        // ...
        var premiumInventory = await _dbContext.PremiumInventories
            .IsNotDeleted()
            .Where(x => x.AccountId == request.AccountId)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotImplementedException();

        if (premiumInventory.PremiumCurrencyAmount < GameConstants.PremiumCurrencyRollCost * request.RollCount)
        {
            throw new NotImplementedException();
        }

        var banner = await _dbContext.Banners
            .IsNotDeleted()
            .Where(x => x.Id == request.BannerId)
            .Select(x => new { x.Id, x.Type })
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotImplementedException();

        var accountBannerData = await _dbContext.AccountBannerStats
            .IsNotDeleted()
            .Where(x => x.AccountId == request.AccountId && x.BannerType == banner.Type)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotImplementedException();

        var bannerCharacters = await _dbContext.BannerCharacters
            .IsNotDeleted()
            .Where(x => x.BannerId == banner.Id)
            .Select(x => new { x.CharacterId, x.Character.Rarity })
            .ToListAsync(cancellationToken);

        var rand = new Random();

        var result = new List<RollData>();

        var legendaries = bannerCharacters
            .Where(x => x.Rarity == CharacterRarity.Legendary)
            .Select(x => x.CharacterId)
            .ToList();

        var epics = bannerCharacters
            .Where(x => x.Rarity == CharacterRarity.Epic)
            .Select(x => x.CharacterId)
            .ToList();

        for (var i = 0; i < request.RollCount; i++)
        {
            var rollResult = rand.Next(1, 100);
            if (rollResult is 100 || accountBannerData.RollsToLegendary <= 0)
            {
                accountBannerData.RollsToLegendary = 100 + 1 + accountBannerData.RollsToLegendary;
                accountBannerData.TotalLegendaryRolls++;
                var charId = rand.Next(0, legendaries.Count - 1);
                result.Add(new RollData
                {
                    Result = RollResult.Legendary,
                    CharacterId = legendaries[charId]
                });
            }
            else if (rollResult >= 90 || accountBannerData.RollsToEpic <= 0)
            {
                accountBannerData.RollsToEpic = 10 + 1 + accountBannerData.RollsToEpic;
                accountBannerData.TotalEpicRolls++;
                var charId = rand.Next(0, epics.Count - 1);
                result.Add(new RollData
                {
                    Result = RollResult.Epic,
                    CharacterId = epics[charId]
                });
            }
            else
            {
                result.Add(new RollData
                {
                    Result = RollResult.Rare,
                    CharacterId = null
                });
            }

            accountBannerData.RollsToLegendary--;
            accountBannerData.RollsToEpic--;
            accountBannerData.TotalRolls++;
        }
        await ProcessResults(request, premiumInventory, result, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RollGachaCommandResult(result);
    }

    private async Task ProcessResults(RollGachaCommand request, PremiumInventory premiumInventory, ICollection<RollData> rolls, CancellationToken cancellationToken)
    {
        var charList = await _dbContext.AccountCharacters
            .Where(x => x.AccountId == request.AccountId)
            .ToListAsync(cancellationToken);

        var rollHistory = new List<AccountBannerHistory>();

        foreach (var rollResult in rolls)
        {
            premiumInventory.PremiumCurrencyAmount -= GameConstants.PremiumCurrencyRollCost;
            var resultString = "";
            switch (rollResult.Result)
            {
                case RollResult.Legendary:
                    // Handle legendary roll
                    resultString = "Легендарный персонаж";
                    await AddCharacter(request, charList, rollResult, cancellationToken);
                    break;
                case RollResult.Epic:
                    // Handle epic roll
                    resultString = "Эпический персонаж";
                    await AddCharacter(request, charList, rollResult, cancellationToken);
                    break;
                case RollResult.Rare:
                    // Handle rare roll
                    resultString = "Редкий предмет: очки талантов на выбор";
                    premiumInventory.WildcardSkillItemCount++;
                    break;
                case RollResult.None:
                    break;
                default:
                    break;
            }
            rollHistory.Add(CreateHistoryEntry(request.AccountId, request.BannerId, resultString));
            _dbContext.AccountBannerHistory.AddRange(rollHistory);
        }
    }

    private async Task AddCharacter(RollGachaCommand request, ICollection<AccountCharacter> charList, RollData roll, CancellationToken cancellationToken)
    {
        if (charList.Any(x => x.CharacterId == roll.CharacterId))
        {
            charList.FirstOrDefault(x => x.CharacterId == roll.CharacterId)!.RepeatCount++;
        }
        else
        {
            var accountCharEntry = new AccountCharacter
            {
                AccountId = request.AccountId,
                CharacterId = roll.CharacterId!.Value,
                RepeatCount = 0,
                FreeSkillPoints = 0,
                TotalSkillPoints = 0,
                CharacterLevel = 1,
                CharacterExperience = 0
            };

            charList.Add(accountCharEntry);

            var defaultAbilities = await _dbContext.DefaultCharacterAbilities
                .IsNotDeleted()
                .Where(x => x.CharacterId == roll.CharacterId)
                .Select(x => new { x.CharacterAbilityId, x.AbilityType })
                .ToListAsync(cancellationToken);

            _dbContext.AccountCharacterAbilities.AddRange(defaultAbilities.Select(x => new AccountCharacterAbility
            {
                AccountCharacter = accountCharEntry,
                CharacterAbilityId = x.CharacterAbilityId,
                AbilityType = x.AbilityType
            }));
        }
    }

    private static AccountBannerHistory CreateHistoryEntry(long accountId, long bannerId, string result)
    {
        return new AccountBannerHistory
        {
            AccountId = accountId,
            BannerId = bannerId,
            Result = result
        };
    }
}
