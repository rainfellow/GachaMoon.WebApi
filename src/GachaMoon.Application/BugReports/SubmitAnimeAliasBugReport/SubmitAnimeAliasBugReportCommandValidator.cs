using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.BugReports.SubmitAnimeAliasBugReport;


public class SubmitAnimeAliasBugReportCommandValidator : AbstractValidator<SubmitAnimeAliasBugReportCommand>
{
    public SubmitAnimeAliasBugReportCommandValidator()
    {
        Include(new AccountValidator());
    }
}