using GachaMoon.Domain.BugReports;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GachaMoon.Database.Configurations.Entities.BugReports;

public class AnimeAliasBugReportConfiguration : ConfigurationBase<AnimeAliasBugReport>
{
    protected override void ApplyConfiguration(EntityTypeBuilder<AnimeAliasBugReport> builder)
    {
        base.ApplyConfiguration(builder);
        _ = builder.Property(x => x.AccountId).IsRequired();
        _ = builder.Property(x => x.AliasId).IsRequired(false);
        _ = builder.Property(x => x.AliasBugReportType).IsRequired();
        _ = builder.Property(x => x.SuggestedAlias).IsRequired().HasMaxLength(100);

        _ = builder.HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey(x => x.AccountId);
    }
}