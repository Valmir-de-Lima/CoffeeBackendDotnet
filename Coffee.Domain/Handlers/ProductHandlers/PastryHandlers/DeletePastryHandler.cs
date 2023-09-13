using Coffee.Domain.Enums;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PastryCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PastryHandlers;

public class DeletePastryHandler : Handler, IHandler<DeletePastryCommand>
{

    private readonly IPastryRepository _repository;

    public DeletePastryHandler(IPastryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(DeletePastryCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var userType = command.GetUserType();

        if ((userType != EType.Manager) && (userType != EType.Barista))
        {
            AddNotification(userType.ToString(), "Informação indisponível");
            return new CommandResult(false, Notifications);
        }

        var pastry = await _repository.GetByDescriptionAsync(command.Description);

        // Query pastry exist
        if (pastry is null)
        {
            AddNotification(command.Description, "Acompanhamento não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Delete database
        _repository.Delete(pastry);

        return new CommandResult(true, new PastryCommandResult(pastry));
    }
}