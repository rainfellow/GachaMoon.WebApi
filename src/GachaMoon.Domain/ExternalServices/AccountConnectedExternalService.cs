using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.ExternalServices;

public class AccountConnectedExternalService : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public ExternalServiceType ExternalServiceType { get; set; }
    public ExternalServiceProvider ExternalServiceProvider { get; set; }
    public string ExternalServiceUserId { get; set; } = default!;

    public virtual Account Account { get; set; } = default!;
}