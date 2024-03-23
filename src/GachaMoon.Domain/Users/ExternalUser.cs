using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Users;

public class ExternalUser : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public string Identifier { get; set; } = default!;
    public ExternalUserType UserType { get; set; }
    public virtual Account Account { get; set; } = default!;
}
