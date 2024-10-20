namespace GachaMoon.Services.Abstractions.Anime.Data;

public record QuizConfigData
{
    public int QuestionNumber { get; set; }
    public bool DiversifyAnime { get; set; }
    public int MinRating { get; set; }
    public int MaxRating { get; set; }
    public int MinReleaseYear { get; set; }
    public int MaxReleaseYear { get; set; }
    public int ImageQuestions { get; set; }
    public int SongQuestions { get; set; }
    public bool AllowOps { get; set; }
    public bool AllowEds { get; set; }
    public bool AllowIns { get; set; }
    public bool AllowTv { get; set; }
    public bool AllowMovie { get; set; }
    public bool AllowSpecial { get; set; }
    public bool AllowOva { get; set; }
    public bool AllowMusic { get; set; }

}

public record MultiplayerQuizConfigData : QuizConfigData
{
    public bool EqualizeQuestions { get; set; }
    public int MinShared { get; set; }
    public int MaxShared { get; set; }
    public PrioritizedSetting PrioritizeSetting { get; set; }
}

public enum PrioritizedSetting
{
    None,
    Diversify,
    Equalize
}