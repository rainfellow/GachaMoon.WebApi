using FluentValidation;
using GachaMoon.Common.Validators;

namespace GachaMoon.Application.Quiz.SubmitQuizFeedback;

public class SubmitQuizFeedbackCommandValidator : AbstractValidator<SubmitQuizFeedbackCommand>
{
    public SubmitQuizFeedbackCommandValidator()
    {
        Include(new AccountValidator());
    }
}