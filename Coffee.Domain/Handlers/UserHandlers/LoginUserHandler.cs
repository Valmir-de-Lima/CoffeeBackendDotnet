using Coffee.Domain.Models.User;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;

namespace Coffee.Domain.Handlers.UserHandlers;

public class LoginUserHandler : Handler, IHandler<LoginUserCommand>
{

    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;


    public LoginUserHandler(IUserRepository repository, ITokenService tokenService, IEmailService emailService)
    {
        _repository = repository;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<ICommandResult> HandleAsync(LoginUserCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        // Build Value Objects
        var email = new Email(command.Email);

        // Get user repository
        var user = await _repository.GetByEmailAsync(email);

        // Query user exist
        if (user == null)
        {
            AddNotification(command.Email, "Usuario ou senha inválidos");
            return new CommandResult(false, Notifications);
        }

        if (!user.VerifyPassword(command.Password))
        {
            AddNotification(command.Password, "Usuario ou senha inválidos");
            return new CommandResult(false, Notifications);
        }

        if (!user.Active)
        {
            AddNotification("user.Active", "Usuario com conta não ativada. Será enviado ao seu e-mail o link para ativação da conta.");
            if (!ActiveUserAccount(user, command))
                AddNotification(user.Id.ToString(), "Não foi possível enviar o email para ativação da conta.");
            return new CommandResult(false, Notifications);
        }

        var token = _tokenService.GenerateToken(user);

        // Procedure for refresh token
        var refreshToken = _tokenService.GenerateRefreshToken();
        _tokenService.DeleteRefreshToken(user.Link);
        await _tokenService.SaveRefreshTokenAsync(user.Link, refreshToken);

        user.UpdateLastLogin();
        _repository.Update(user);

        return new CommandResult(true, new
        {
            userName = user.Link,
            token,
            refreshToken
        });
    }

    private bool ActiveUserAccount(User user, LoginUserCommand command)
    {
        if (!_emailService.Send(user.Name, user.Email.Address, "Ativação da conta", FormatEmailBody(user, command.GetUrlOfSite())))
        {
            return false;
        }
        return true;
    }

    private string FormatEmailBody(User user, string urlOfSite)
    {
        var body = $"Olá, <strong>{user.Name}</strong>! "
        + "<p>Clique <a href=\"" + urlOfSite + "/v1/users/login/active/" + user.Id + "\">aqui</a> para ativar a sua conta.</p>";
        return body;
    }
}