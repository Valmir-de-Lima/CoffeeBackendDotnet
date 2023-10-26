using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.OrderCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces.Orders;

namespace Coffee.Domain.Handlers.OrderHandlers;

public class GetOrderHandler : Handler, IHandler<GetOrderCommand>
{

    private readonly IOrderRepository _repository;

    public GetOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetOrderCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var order = await _repository.GetByIdWithItemsAsync(new Guid(command.OrderId));

        // Query personalized coffee exist
        if (order is null)
        {
            AddNotification(command.OrderId, "Pagamento não cadastrado");
            return new CommandResult(false, Notifications);
        }

        return new CommandResult(true, new OrderCommandResult(order));
    }
}