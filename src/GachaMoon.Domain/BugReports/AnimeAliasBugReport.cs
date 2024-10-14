// src/GachaMoon.Domain/Banners/Banner.cs
using GachaMoon.Domain.Accounts;
using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.BugReports;

public class AnimeAliasBugReport : SoftDeleteEntityBase<long>
{
    public long AccountId { get; set; }
    public virtual Account Account { get; set; } = default!;
    public string SuggestedAlias { get; set; } = default!;
    public string SuggestedLanguage { get; set; } = default!;
    public AliasBugReportType AliasBugReportType { get; set; }
    public long? AliasId { get; set; }
}

public enum AliasBugReportType
{
    None,
    MissingTitle,
    WrongTitle,
    WrongLanguage
}