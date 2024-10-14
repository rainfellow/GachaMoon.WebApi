using GachaMoon.Services.Abstractions.Auth;
using GachaMoon.Services.Abstractions.Clients.Data;

namespace GachaMoon.Application.User.External.DiscordSignup;

public class DiscordSignUpCommandHandler(IDiscordSignupService discordSignupService) : IRequestHandler<DiscordSignUpCommand, DiscordSignUpCommandResult>
{
    private readonly IDiscordSignupService _discordSignupService = discordSignupService;

    public async Task<DiscordSignUpCommandResult> Handle(DiscordSignUpCommand request, CancellationToken cancellationToken)
    {
        await _discordSignupService.RegisterDiscordUser(new DiscordUserData
        {
            Id = request.DiscordIdentifier,
            Username = request.AccountName
        });

        return new DiscordSignUpCommandResult();
    }
}
