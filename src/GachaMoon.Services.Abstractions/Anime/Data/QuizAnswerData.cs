namespace GachaMoon.Services.Abstractions.Anime.Data;
public record QuizAnswerData
{
    public string CorrectAnswer { get; set; } = default!;
    public string PredictedAnswer { get; set; } = default!;
    public bool Result { get; set; } = default!;

    public QuizAnswerData(string correctAnswer, string predictedAnswer, bool result)
    {
        CorrectAnswer = correctAnswer;
        PredictedAnswer = predictedAnswer;
        Result = result;
    }
}