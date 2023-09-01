using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;

namespace Coffee.Domain.Handlers.UserHandlers;

public class RefreshLoginUserHandler : Handler, IHandler<RefreshLoginUserCommand>
{

    private readonly ITokenService _tokenService;

    public RefreshLoginUserHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<ICommandResult> HandleAsync(RefreshLoginUserCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var userClaims = _tokenService.GetClaimsFromToken(command.Token);
        var userName = userClaims.Identity!.Name;
        var savedRefreshToken = await _tokenService.GetRefreshTokenAsync(userName!);

        // Query refreshToken valid
        if (savedRefreshToken != command.RefreshToken)
        {
            AddNotification(command.RefreshToken, "Refresh Token inválido");
            return new CommandResult(false, Notifications);
        }


        // Procedure for refresh token
        var newToken = _tokenService.GenerateToken(userClaims.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        _tokenService.DeleteRefreshToken(userName!);
        await _tokenService.SaveRefreshTokenAsync(userName!, newRefreshToken);


        return new CommandResult(true, new
        {
            userName,
            newToken,
            newRefreshToken
        });
    }
}