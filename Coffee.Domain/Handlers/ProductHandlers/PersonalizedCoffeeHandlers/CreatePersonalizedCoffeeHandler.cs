using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

public class CreatePersonalizedCoffeeHandler : Handler, IHandler<CreatePersonalizedCoffeeCommand>
{

    private readonly IPersonalizedCoffeeRepository _repository;

    public CreatePersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePersonalizedCoffeeCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        decimal priceCoffe;
        decimal.TryParse(command.PriceCoffe, out priceCoffe);

        // Build entity
        var personalizedCoffee = new PersonalizedCoffee(
            new Guid(command.CustomerId),
            new Guid(command.CoffeId),
            command.DescriptionCoffe,
            priceCoffe);

        // Save database
        await _repository.CreateAsync(personalizedCoffee);

        return new CommandResult(true, new PersonalizedCoffeeCommandResult(personalizedCoffee));
    }
}