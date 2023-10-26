using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.PaymentCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Repositories.Interfaces.Orders;

namespace Coffee.Domain.Handlers.PaymentHandlers;

public class PaymentHandler : Handler,
    IHandler<CreatePaymentCommand>,
    IHandler<GetPaymentCommand>
{
    private readonly IPaymentRepository _repository;
    private readonly IBasketRepository _basketRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public PaymentHandler(
        IPaymentRepository repository,
        IBasketRepository basketRepository,
        IOrderRepository orderRepository,
        IUserRepository userRepository
        )
    {
        _repository = repository;
        _basketRepository = basketRepository;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePaymentCommand command)
    {
        return await new CreatePaymentHandler(_repository, _basketRepository, _orderRepository, _userRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetPaymentCommand command)
    {
        return await new GetPaymentHandler(_repository).HandleAsync(command);
    }
}