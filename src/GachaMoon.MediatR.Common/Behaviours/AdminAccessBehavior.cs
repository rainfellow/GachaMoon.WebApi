using GachaMoon.Common.Contracts;
using GachaMoon.Common.DatabaseExtensions;
using GachaMoon.Database;

namespace GachaMoon.MediatR.Common.Behaviours;

public class AdminAccessBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IAdminRequest
{
    private readonly ApplicationDbContext _dbContext;

    public AdminAccessBehavior(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _dbContext.InternalUsers
            .ThrowIfUserNotAdmin(request.UserId);

        return await next();
    }
}
