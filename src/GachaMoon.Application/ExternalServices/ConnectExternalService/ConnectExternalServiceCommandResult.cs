
namespace GachaMoon.Application.ExternalServices.ConnectExternalService;

public record ConnectExternalServiceCommandResult()
{
    public string ExternalServiceUserId { get; init; } = default!;
}