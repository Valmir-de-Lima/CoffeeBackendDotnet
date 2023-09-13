using Coffee.Domain.Enums;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.CoffeHandlers;

public class CreateCoffeHandler : Handler, IHandler<CreateCoffeCommand>
{

    private readonly ICoffeRepository _repository;

    public CreateCoffeHandler(ICoffeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreateCoffeCommand command)
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
        if (coffe is not null)
        {
            AddNotification(command.Description, "Café já cadastrado");
            return new CommandResult(false, Notifications);
        }

        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(command.Price, out priceDecimal);
        bool.TryParse(command.Active, out activeBool);


        // Build entity
        coffe = new Coffe(command.Description, priceDecimal, activeBool);

        // Save database
        await _repository.CreateAsync(coffe);

        return new CommandResult(true, new CoffeCommandResult(coffe));
    }
}