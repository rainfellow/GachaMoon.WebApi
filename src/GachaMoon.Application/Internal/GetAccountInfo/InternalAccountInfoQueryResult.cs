namespace GachaMoon.Application.Internal.GetAccountInfo;

public record InternalAccountInfoQueryResult
{
    public long AccountId { get; set; }
    public string AccountName { get; set; } = default!;
}
