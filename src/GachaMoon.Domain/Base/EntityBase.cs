using NodaTime;

namespace GachaMoon.Domain.Base;

public class EntityBase<TKey> where TKey : struct
{
    public TKey Id { get; set; }
    public Instant CreatedAt { get; set; }
    public Instant UpdatedAt { get; set; }
}
