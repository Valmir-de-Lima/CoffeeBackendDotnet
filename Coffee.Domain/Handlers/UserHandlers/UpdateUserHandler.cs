using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.UserHandlers;

public class UpdateUserHandler : Handler, IHandler<UpdateUserCommand>
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(UpdateUserCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var link = command.GetUserName();

        if (link == null)
        {
            AddNotification(link, "Identificacao do usuário não disponível");
            return new CommandResult(false, Notifications);
        }
        // Get user repository
        var user = await _repository.GetByLinkAsync(link);

        // Query user exist
        if (user == null)
        {
            AddNotification(link, "Usuário não cadastrado");
            return new CommandResult(false, Notifications);
        }

        user.UpdateName(command.Name);

        // Save database
        _repository.Update(user);

        return new CommandResult(true, new UserCommandResult(user));
    }
}