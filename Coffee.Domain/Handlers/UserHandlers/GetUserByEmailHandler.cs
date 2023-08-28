using Coffee.Domain.Enums;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.UserHandlers;

public class GetUserByEmailHandler : Handler
{

    private readonly IUserRepository _repository;

    public GetUserByEmailHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetUserByEmailCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var linkToken = command.GetUserName();
        var linkCommand = command.Email.Replace("@", "-").Replace(".", "-");
        var manager = command.GetUserType();

        if ((linkToken != linkCommand) && (manager != EType.Manager))
        {
            AddNotification(command.Email, "Informação indisponível");
            return new CommandResult(false, Notifications);
        }

        // Get user repository
        var user = await _repository.GetByEmailAsync(new Email(command.Email));

        // Query user exist
        if (user == null)
        {
            AddNotification(command.Email, "Usuario não cadastrado");
            return new CommandResult(false, Notifications);
        }

        return new CommandResult(true, new UserCommandResult(user));
    }
}