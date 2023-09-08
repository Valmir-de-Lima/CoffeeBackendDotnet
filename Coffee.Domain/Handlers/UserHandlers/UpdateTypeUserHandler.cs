using Coffee.Domain.Enums;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.UserHandlers;

public class UpdateTypeUserHandler : Handler, IHandler<UpdateTypeUserCommand>
{

    private readonly IUserRepository _repository;

    public UpdateTypeUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(UpdateTypeUserCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var managerType = command.GetUserType();

        if (managerType != EType.Manager)
        {
            AddNotification("command.GetUserType", "Informação indisponível");
            return new CommandResult(false, Notifications);
        }

        var email = new Email(command.Email);

        // Get user repository
        var user = await _repository.GetByEmailAsync(email);

        // Query user exist
        if (user == null)
        {
            AddNotification(command.Email, "Usuário não cadastrado");
            return new CommandResult(false, Notifications);
        }

        user.UpdateType((EType)command.Type);

        // Save database
        _repository.Update(user);

        return new CommandResult(true, new UserCommandResult(user));
    }
}