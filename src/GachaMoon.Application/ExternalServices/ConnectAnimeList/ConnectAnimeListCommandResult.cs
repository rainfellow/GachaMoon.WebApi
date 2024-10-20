
namespace GachaMoon.Application.ExternalServices.ConnectAnimeList;

public record ConnectAnimeListCommandResult()
{
    public string ExternalServiceUserId { get; init; } = default!;
    public int AnimeCount { get; init; } = default!;
}