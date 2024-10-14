using GachaMoon.Application.BugReports.SubmitAnimeAliasBugReport;
using GachaMoon.Domain.BugReports;

namespace GachaMoon.WebApi.Endpoints.Application.BugReports.Models;

public record SubmitAliasBugReportRequest(string SuggestedAlias, string SuggestedLanguage, AliasBugReportType ReportType, long? AliasId = null)
{
    public SubmitAnimeAliasBugReportCommand ToCommand(long AccountId)
    {
        return new SubmitAnimeAliasBugReportCommand(AccountId, SuggestedAlias, SuggestedLanguage, ReportType, AliasId);
    }
}
