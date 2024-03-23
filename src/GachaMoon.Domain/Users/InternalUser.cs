using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Users;

public class InternalUser : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public string Email { get; set; } = default!;
    public byte[] Password { get; set; } = default!;
    public virtual Account Account { get; set; } = default!;
}
