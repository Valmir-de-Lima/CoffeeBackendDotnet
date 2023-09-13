using Coffee.Domain.Enums;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.CoffeHandlers;

public class UpdateCoffeHandler : Handler, IHandler<UpdateCoffeCommand>
{

    private readonly ICoffeRepository _repository;

    public UpdateCoffeHandler(ICoffeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(UpdateCoffeCommand command)
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

        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(command.Price, out priceDecimal);
        bool.TryParse(command.Active, out activeBool);


        // Update entity
        coffe.Update(command.Description, priceDecimal, activeBool);

        // Save database
        _repository.Update(coffe);

        return new CommandResult(true, new CoffeCommandResult(coffe));
    }
}