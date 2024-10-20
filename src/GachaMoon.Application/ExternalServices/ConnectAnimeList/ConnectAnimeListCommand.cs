using GachaMoon.Common.Contracts;
using GachaMoon.Domain.ExternalServices;

namespace GachaMoon.Application.ExternalServices.ConnectAnimeList;

public class ConnectAnimeListCommand(long accountId, ExternalServiceType serviceType, ExternalServiceProvider serviceProvider, string externalServiceUserId, ICollection<string> allowedListGroups)
 : IRequest<ConnectAnimeListCommandResult>, IAccountRequest
{
    public long AccountId { get; init; } = accountId;
    public ExternalServiceType ServiceType { get; init; } = serviceType;
    public ExternalServiceProvider ServiceProvider { get; init; } = serviceProvider;
    public string ExternalServiceUserId { get; init; } = externalServiceUserId;
    public ICollection<string> AllowedListGroups { get; init; } = allowedListGroups;
}
