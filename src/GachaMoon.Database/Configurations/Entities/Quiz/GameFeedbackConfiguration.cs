using GachaMoon.Database.Extensions;
using GachaMoon.Domain.Quiz;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.Quiz;

public class GameFeedbackConfiguration : ConfigurationBase<GameFeedback>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<GameFeedback> builder)
    {
        base.ApplyConfiguration(builder);

        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.GameResultId).IsRequired();
        _ = builder.Property(x => x.Feedback).HasJsonConversion().IsRequired();
        _ = builder.HasIndex(x => new { x.AccountId, x.GameResultId }).WhereNotDeleted().IsUnique();
    }
}