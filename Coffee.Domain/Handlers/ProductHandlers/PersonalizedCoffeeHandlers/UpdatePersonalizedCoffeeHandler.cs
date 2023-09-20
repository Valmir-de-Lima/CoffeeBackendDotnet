using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

public class UpdatePersonalizedCoffeeHandler : Handler, IHandler<UpdatePersonalizedCoffeeCommand>
{
    private readonly IPersonalizedCoffeeRepository _repository;

    public UpdatePersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(UpdatePersonalizedCoffeeCommand command)
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

        decimal priceCoffe;
        decimal.TryParse(command.PriceCoffe, out priceCoffe);

        // Build entity
        personalizedCoffee.Update(
            new Guid(command.CoffeId),
            command.DescriptionCoffe,
            priceCoffe);

        // Save database        
        _repository.Update(personalizedCoffee);

        return new CommandResult(true, new PersonalizedCoffeeCommandResult(personalizedCoffee));
    }
}