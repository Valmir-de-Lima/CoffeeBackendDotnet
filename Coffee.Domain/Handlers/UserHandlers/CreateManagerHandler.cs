using Coffee.Domain.Models.User;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Services;

namespace Coffee.Domain.Handlers.UserHandlers;

public class CreateManagerHandler : Handler, IHandler<CreateManagerCommand>
{
    private readonly IUserRepository _repository;
    private readonly IEmailService _emailService;

    public CreateManagerHandler(IUserRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public async Task<ICommandResult> HandleAsync(CreateManagerCommand command)
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

        // Query e-mail exist
        if (await _repository.ExistsEmailAsync(email))
        {
            AddNotification(command.Email, "Email já cadastrado");
            return new CommandResult(false, Notifications);
        }

        var passwordHash = new Password(command.Password);

        // Build entity
        var user = new User(command.Name, email, passwordHash, command.Type);

        // Send user email
        if (!_emailService.Send(command.Name, command.Email, "Bem vindo a Loja!", FormatEmailBody(user, command.GetUrlOfSite())))
        {
            AddNotification(command.GetUrlOfSite(), "Não foi possível enviar o email para registro");
            return new CommandResult(false, Notifications);
        }

        // Save database
        await _repository.CreateAsync(user);

        return new CommandResult(true, new UserCommandResult(user));
    }

    private string FormatEmailBody(User user, string urlOfSite)
    {
        var body = $"Olá, <strong>{user.Name}</strong>! "
        + "<p>Clique <a href=\"" + urlOfSite + "/v1/users/login/active/" + user.Id + "\">aqui</a> para ativar a sua conta.</p>";
        return body;
    }
}