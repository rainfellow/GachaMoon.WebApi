using GachaMoon.Domain.Base;

namespace GachaMoon.Domain.Quiz;

public class GameResult : SoftDeleteEntityBase<long>
{
    public string Title { get; set; } = default!;
    public GameType GameType { get; set; } = default!;
    public GameRecap GameRecap { get; set; } = default!;
}

public enum GameType
{
    None,
    SoloGame,
    Multiplayer
}