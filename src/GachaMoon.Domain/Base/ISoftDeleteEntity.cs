using NodaTime;

namespace GachaMoon.Domain.Base;

public interface ISoftDeleteEntity
{
    public Instant? DeletedAt { get; set; }
}
