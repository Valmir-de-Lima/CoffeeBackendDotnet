using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.UserHandlers;

public class ConfirmRecoveryPasswordUserHandler : Handler
{

    private readonly IUserRepository _repository;

    public ConfirmRecoveryPasswordUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(ConfirmRecoveryPasswordUserCommand command)
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
            AddNotification(command.Id.ToString(), "Usuario não cadastrado");
            return new CommandResult(false, Notifications);
        }

        user.UpdateActive(false);
        _repository.Update(user);

        return new CommandResult(true, new UserCommandResult(user));
    }
}