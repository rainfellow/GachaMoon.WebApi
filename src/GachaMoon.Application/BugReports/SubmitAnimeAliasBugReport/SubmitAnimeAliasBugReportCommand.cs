using GachaMoon.Common.Contracts;
using GachaMoon.Domain.BugReports;

namespace GachaMoon.Application.BugReports.SubmitAnimeAliasBugReport;

public class SubmitAnimeAliasBugReportCommand(long accountId, string suggestedAlias, string suggestedLanguage, AliasBugReportType reportType, long? aliasId) : IRequest, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public string SuggestedAlias { get; init; } = suggestedAlias;
    public string SuggestedLanguage { get; init; } = suggestedLanguage;
    public AliasBugReportType ReportType { get; init; } = reportType;
    public long? AliasId { get; init; } = aliasId;
}
