using Coffee.Domain.Enums;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.UserHandlers;

public class GetUserHandler : Handler
{

    private readonly IUserRepository _repository;

    public GetUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetUserCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var linkToken = command.GetUserName();
        var manager = command.GetUserType();

        if ((linkToken != command.Link) && (manager != EType.Manager))
        {
            AddNotification(command.Link, "Informação indisponível");
            return new CommandResult(false, Notifications);
        }

        // Get user repository
        var user = await _repository.GetByLinkAsync(command.Link);

        // Query user exist
        if (user == null)
        {
            AddNotification(command.Link, "Usuario não cadastrado");
            return new CommandResult(false, Notifications);
        }

        return new CommandResult(true, new UserCommandResult(user));
    }
}