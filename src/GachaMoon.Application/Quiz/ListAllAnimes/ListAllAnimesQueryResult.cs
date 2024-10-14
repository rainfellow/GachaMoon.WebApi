using GachaMoon.Application.Quiz.Common;

namespace GachaMoon.Application.Quiz.ListAllAnimes;

public record ListAllAnimesQueryResult
{
    public required ICollection<AnimeData> AnimeData { get; init; }
}
