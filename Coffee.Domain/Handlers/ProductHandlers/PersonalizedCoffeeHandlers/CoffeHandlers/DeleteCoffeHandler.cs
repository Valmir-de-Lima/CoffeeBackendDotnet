using Coffee.Domain.Enums;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.CoffeHandlers;

public class DeleteCoffeHandler : Handler, IHandler<DeleteCoffeCommand>
{

    private readonly ICoffeRepository _repository;

    public DeleteCoffeHandler(ICoffeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(DeleteCoffeCommand command)
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

        var coffe = await _repository.GetByDescriptionAsync(command.Description);

        // Query coffee exist
        if (coffe is null)
        {
            AddNotification(command.Description, "Café não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Delete database
        _repository.Delete(coffe);

        return new CommandResult(true, new CoffeCommandResult(coffe));
    }
}