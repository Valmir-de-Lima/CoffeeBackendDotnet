using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.BasketHandlers;

public class DeleteBasketHandler : Handler, IHandler<DeleteBasketCommand>
{

    private readonly IBasketRepository _repository;

    public DeleteBasketHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(DeleteBasketCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var basket = await _repository.GetByIdAsync(new Guid(command.BasketId));

        // Query personalized coffee exist
        if (basket is null)
        {
            AddNotification(command.BasketId, "Cesta de compras não cadastrada");
            return new CommandResult(false, Notifications);
        }

        // Delete database
        _repository.Delete(basket);

        return new CommandResult(true, new BasketCommandResult(basket));
    }
}