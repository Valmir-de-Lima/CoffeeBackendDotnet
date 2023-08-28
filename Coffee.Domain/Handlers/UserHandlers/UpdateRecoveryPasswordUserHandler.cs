using Coffee.Domain.Models.User;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;

namespace Coffee.Domain.Handlers.UserHandlers;

public class UpdateRecoveryPasswordUserHandler : Handler, IHandler<UpdateRecoveryPasswordUserCommand>
{

    private readonly IUserRepository _repository;
    private readonly IEmailService _emailService;

    public UpdateRecoveryPasswordUserHandler(IUserRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public async Task<ICommandResult> HandleAsync(UpdateRecoveryPasswordUserCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        // Get user repository
        var user = await _repository.GetByIdAsync(new Guid(command.Id));

        // Query user exist
        if (user == null)
        {
            AddNotification(command.Id.ToString(), "Operação inválida");
            return new CommandResult(false, Notifications);
        }

        // Query active user
        if (user.Active)
        {
            AddNotification("Active", "Operação inválida");
            user.UpdateRecoveryPassword("");
            _repository.Update(user);
            return new CommandResult(false, Notifications);
        }

        // Query recovery password
        if (!user.VerifyRecoveryPassword(command.RecoveryPassword))
        {
            AddNotification(command.RecoveryPassword.ToString(), "Código de autenticação inválido. Repeta o processo de recuperação de senha.");
            user.UpdateRecoveryPassword("");
            _repository.Update(user);
            return new CommandResult(false, Notifications);
        }

        // Send user email
        if (!_emailService.Send(user.Name, user.Email.Address, "Conclusão da criação da senha", FormatEmailBody(user, command.GetUrlOfSite())))
        {
            AddNotification(command.GetUrlOfSite(), "Não foi possível enviar o email para ativação da conta.");
            return new CommandResult(false, Notifications);
        }

        var passwordHash = new Password(command.Password);
        // Save database
        user.UpdatePassword(passwordHash);
        user.UpdateRecoveryPassword("");
        _repository.Update(user);

        return new CommandResult(true, new UserCommandResult(user));
    }

    private string FormatEmailBody(User user, string urlOfSite)
    {
        var body = $"Olá, <strong>{user.Name}</strong>! "
        + "<p>Clique <a href=\"" + urlOfSite + "/v1/users/login/active/" + user.Id + "\">aqui</a> para ativar a sua conta.</p>";
        return body;
    }
}