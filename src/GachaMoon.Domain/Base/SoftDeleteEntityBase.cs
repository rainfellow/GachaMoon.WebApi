using NodaTime;

namespace GachaMoon.Domain.Base;

public abstract class SoftDeleteEntityBase<TKey> : EntityBase<TKey>, ISoftDeleteEntity
    where TKey : struct
{
    public Instant? DeletedAt { get; set; }
}
