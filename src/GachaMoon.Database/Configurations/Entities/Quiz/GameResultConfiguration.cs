using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Quiz;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Quiz;

public class GameResultConfuguration : ConfigurationBase<GameResult>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<GameResult> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.Title).IsRequired();
        _ = builder.Property(x => x.GameType).IsRequired();
        _ = builder.Property(x => x.GameRecap).HasJsonConversion().IsRequired();
        _ = builder.HasIndex(x => new { x.Title }).WhereNotDeleted().IsUnique();
    }
}