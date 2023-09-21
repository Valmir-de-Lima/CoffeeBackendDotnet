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
    private readonly IUserRepository _userRepository;
    private readonly ICoffeRepository _coffeRepository;

    public CreatePersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository, IUserRepository userRepository, ICoffeRepository coffeRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _coffeRepository = coffeRepository;
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

        var user = await _userRepository.GetByIdAsync(new Guid(command.CustomerId));

        // Query customer exist
        if (user is null)
        {
            AddNotification(command.CustomerId, "Cliente não cadastrado");
            return new CommandResult(false, Notifications);
        }

        var coffee = await _coffeRepository.GetByIdAsync(new Guid(command.CoffeId));

        // Query coffee exist
        if (coffee is null)
        {
            AddNotification(command.CustomerId, "Café não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Query coffee available
        if (!coffee.Active)
        {
            AddNotification(command.CoffeId, "Café não disponível");
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