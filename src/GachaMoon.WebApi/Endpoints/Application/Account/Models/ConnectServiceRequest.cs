using GachaMoon.Application.ExternalServices.ConnectAnimeList;
using GachaMoon.Domain.ExternalServices;

namespace GachaMoon.WebApi.Endpoints.Application.Account.Models;

public record ConnectServiceRequest
{
    public ExternalServiceType ServiceType { get; set; } = default!;
    public ExternalServiceProvider ServiceProvider { get; set; } = default!;
    public string ExternalServiceUserId { get; set; } = default!;
    public ICollection<string> AllowedListGroups { get; set; } = default!;

    public ConnectAnimeListCommand ToCommand(long accountId)
    {
        return new ConnectAnimeListCommand(accountId, ServiceType, ServiceProvider, ExternalServiceUserId, AllowedListGroups);
    }
}
