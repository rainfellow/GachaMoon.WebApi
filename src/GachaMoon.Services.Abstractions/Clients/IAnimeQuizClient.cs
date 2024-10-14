using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Services.Abstractions.Clients;

public interface IAnimeQuizClient
{
    public Task<AnimeQuizResult> GetQuizResult(string answer, CancellationToken cancellationToken = default);
}
