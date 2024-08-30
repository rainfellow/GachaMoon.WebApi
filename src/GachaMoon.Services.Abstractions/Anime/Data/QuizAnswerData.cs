namespace GachaMoon.Services.Abstractions.Anime.Data;
public record QuizAnswerData
{
    public string CorrectAnswer { get; set; } = default!;
    public bool Result { get; set; } = default!;

    public QuizAnswerData(string correctAnswer, bool result)
    {
        CorrectAnswer = correctAnswer;
        Result = result;
    }
}