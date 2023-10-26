using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.OrderCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Repositories.Interfaces.Orders;

namespace Coffee.Domain.Handlers.OrderHandlers;

public class OrderHandler : Handler,
    IHandler<GetOrderCommand>
{
    private readonly IOrderRepository _repository;
    public OrderHandler(
        IOrderRepository repository
        )
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetOrderCommand command)
    {
        return await new GetOrderHandler(_repository).HandleAsync(command);
    }
}