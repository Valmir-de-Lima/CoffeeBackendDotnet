using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

public class DeletePersonalizedCoffeeHandler : Handler, IHandler<DeletePersonalizedCoffeeCommand>
{

    private readonly IPersonalizedCoffeeRepository _repository;

    public DeletePersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(DeletePersonalizedCoffeeCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var personalizedCoffee = await _repository.GetByIdAsync(new Guid(command.PersonalizedCoffeeId));

        // Query personalized coffee exist
        if (personalizedCoffee is null)
        {
            AddNotification(command.PersonalizedCoffeeId, "Café personalizado não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Delete database
        _repository.Delete(personalizedCoffee);

        return new CommandResult(true, new PersonalizedCoffeeCommandResult(personalizedCoffee));
    }
}