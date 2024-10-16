namespace GachaMoon.Application.Internal.GetAccountInfo;

public class InternalAccountInfoQuery(long accountId) : IRequest<InternalAccountInfoQueryResult>
{
    public long AccountId { get; init; } = accountId;
}
