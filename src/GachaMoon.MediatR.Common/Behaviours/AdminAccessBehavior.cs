using GachaMoon.Common.Contracts;
using GachaMoon.Common.DatabaseExtensions;
using GachaMoon.Database;

namespace GachaMoon.MediatR.Common.Behaviours;

public class AdminAccessBehavior<TRequest, TResponse>(ApplicationDbContext dbContext) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IAdminRequest
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _dbContext.Accounts
            .ThrowIfUserNotAdmin(request.AccountId);

        return await next();
    }
}
