using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.Promocodes;
using GachaMoon.Services.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Promocodes.Redeem;

public class RedeemPromocodeCommandHandler(ApplicationDbContext dbContext, IClockProvider clockProvider) : IRequestHandler<RedeemPromocodeCommand, RedeemPromocodeCommandResult>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IClockProvider _clockProvider = clockProvider;

    public async Task<RedeemPromocodeCommandResult> Handle(RedeemPromocodeCommand request, CancellationToken cancellationToken)
    {
        var now = _clockProvider.Now;
        var code = await _dbContext.Promocodes
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.Code == request.SubmittedCode, cancellationToken)
            ?? throw new NotImplementedException($"Promocode {request.SubmittedCode} not found.");

        if (code.ExpiryDate < DateOnly.FromDateTime(now.ToDateTimeUtc()))
        {
            throw new NotImplementedException();
        }

        if (code.UsesLeft < 1)
        {
            throw new NotImplementedException();
        }

        var codeEffects = await _dbContext.PromocodeEffects
            .IsNotDeleted()
            .Where(x => x.PromoCodeId == code.Id)
            .Select(x => new
            {
                x.EffectType,
                x.EffectAmount
            })
            .ToListAsync(cancellationToken);

        var usedAlready = await _dbContext.PromocodeHistory
            .IsNotDeleted()
            .Where(x => x.PromoCodeId == code.Id && x.AccountId == request.AccountId)
            .AnyAsync(cancellationToken);

        if (usedAlready)
        {
            throw new NotImplementedException();
        }

        _dbContext.PromocodeHistory.Add(CreateHistoryEntry(code.Id, request.AccountId));

        var accountPremiumInventory = await _dbContext.PremiumInventories
            .IsNotDeleted()
            .FirstOrDefaultAsync(x => x.AccountId == request.AccountId, cancellationToken) ?? throw new NotImplementedException();
        foreach (var effect in codeEffects)
        {
            switch (effect.EffectType)
            {
                case PromocodeEffectType.GivePremiumCurrency:
                    accountPremiumInventory.PremiumCurrencyAmount += effect.EffectAmount;
                    break;
                case PromocodeEffectType.GiveStandardRolls:
                    accountPremiumInventory.StandardBannerRollsAmount += effect.EffectAmount;
                    break;
                case PromocodeEffectType.GiveCharacter:
                    break;
                case PromocodeEffectType.None:
                    break;
                default:
                    break;
            }
        }

        code.UsesLeft--;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new()
        {
        };
    }

    private static PromocodeHistory CreateHistoryEntry(long codeId, long accountId)
    {
        return new PromocodeHistory
        {
            AccountId = accountId,
            PromoCodeId = codeId
        };
    }
}
