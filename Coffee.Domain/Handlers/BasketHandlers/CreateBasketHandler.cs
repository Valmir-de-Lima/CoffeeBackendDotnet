using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Models.Baskets;

namespace Coffee.Domain.Handlers.BasketHandlers;

public class CreateBasketHandler : Handler, IHandler<CreateBasketCommand>
{
    private readonly IBasketRepository _repository;
    private readonly IUserRepository _userRepository;

    public CreateBasketHandler(
        IBasketRepository repository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreateBasketCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var customer = await _userRepository.GetByIdAsync(new Guid(command.CustomerId));

        // Query basket exist
        if (customer is null)
        {
            AddNotification(command.CustomerId, "Cliente não cadastrado");
            return new CommandResult(false, Notifications);
        }

        var basket = new Basket(new Guid(command.CustomerId));

        // Save database
        await _repository.CreateAsync(basket);

        return new CommandResult(true, new BasketCommandResult(basket));
    }
}