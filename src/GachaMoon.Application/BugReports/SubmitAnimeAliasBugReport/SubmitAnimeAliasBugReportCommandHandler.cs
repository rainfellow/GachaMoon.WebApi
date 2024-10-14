using GachaMoon.Database;
using GachaMoon.Domain.BugReports;
using GachaMoon.Services.Abstractions.Time;

namespace GachaMoon.Application.BugReports.SubmitAnimeAliasBugReport;


public class SubmitAnimeAliasBugReportCommandHandler(ApplicationDbContext dbContext, IClockProvider clockProvider) : IRequestHandler<SubmitAnimeAliasBugReportCommand>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IClockProvider _clockProvider = clockProvider;

    public async Task Handle(SubmitAnimeAliasBugReportCommand request, CancellationToken cancellationToken)
    {
        var now = _clockProvider.Now;
        var newReport = new AnimeAliasBugReport
        {
            AccountId = request.AccountId,
            SuggestedAlias = request.SuggestedAlias,
            SuggestedLanguage = request.SuggestedLanguage,
            AliasBugReportType = request.ReportType,
            AliasId = request.AliasId,
            CreatedAt = now,
            UpdatedAt = now
        };
        await _dbContext.AnimeAliasBugReports.AddAsync(newReport, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
