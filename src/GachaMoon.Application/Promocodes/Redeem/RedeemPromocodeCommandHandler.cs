using GachaMoon.Common.Query;
using GachaMoon.Database;
using GachaMoon.Domain.Promocodes;
using GachaMoon.Services.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Application.Promocodes.Redeem;

public class RedeemPromocodeCommandHandler : IRequestHandler<RedeemPromocodeCommand, RedeemPromocodeCommandResult>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IClockProvider _clockProvider;

    public RedeemPromocodeCommandHandler(ApplicationDbContext dbContext, IClockProvider clockProvider)
    {
        _dbContext = dbContext;
        _clockProvider = clockProvider;
    }

    public async Task<RedeemPromocodeCommandResult> Handle(RedeemPromocodeCommand request, CancellationToken cancellationToken)
    {
        var now = _clockProvider.Now;
        var code = await _dbContext.Promocodes
            .IsNotDeleted()
            .Select(x => new
            {
                x.Id,
                x.ExpiryDate,
                x.UsesLeft,
                x.Code
            })
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotImplementedException($"Promocode {request.SubmittedCode} not found.");

        if (code.ExpiryDate < DateOnly.FromDateTime(now.ToDateTimeUtc()))
        {
            throw new NotImplementedException();
        }

        if (code.UsesLeft < 1)
        {
            throw new NotImplementedException();
        }

        var usedAlready = await _dbContext.PromocodeHistory
            .IsNotDeleted()
            .Where(x => x.PromoCodeId == code.Id && x.AccountId == request.AccountId)
            .AnyAsync(cancellationToken);

        if (usedAlready)
        {
            throw new NotImplementedException();
        }

        _dbContext.PromocodeHistory.Add(CreateHistoryEntry(code.Id, request.AccountId));


        //todo better promocode system
        if (code.Code == "APRILFOOLS")
        {
            var accountPremiumInventory = await _dbContext.PremiumInventories
                .IsNotDeleted()
                .Where(x => x.AccountId == request.AccountId)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new NotImplementedException();

            accountPremiumInventory.PremiumCurrencyAmount += 1000;
        }

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
