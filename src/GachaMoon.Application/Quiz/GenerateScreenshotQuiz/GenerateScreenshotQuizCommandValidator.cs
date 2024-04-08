using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Quiz.GenerateScreenshotQuiz;

public class GenerateScreenshotQuizCommandValidator : AbstractValidator<GenerateScreenshotQuizCommand>
{
    public GenerateScreenshotQuizCommandValidator()
    {
        Include(new AccountValidator());
    }
}