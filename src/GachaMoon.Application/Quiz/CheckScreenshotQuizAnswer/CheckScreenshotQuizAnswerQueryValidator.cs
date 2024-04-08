using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Quiz.CheckScreenshotQuizAnswer;

public class CheckScreenshotQuizAnswerQueryValidator : AbstractValidator<CheckScreenshotQuizAnswerQuery>
{
    public CheckScreenshotQuizAnswerQueryValidator()
    {
        Include(new AccountValidator());
    }
}