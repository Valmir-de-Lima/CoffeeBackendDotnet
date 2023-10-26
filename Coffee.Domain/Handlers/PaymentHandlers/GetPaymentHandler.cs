using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.PaymentCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.PaymentHandlers;

public class GetPaymentHandler : Handler, IHandler<GetPaymentCommand>
{

    private readonly IPaymentRepository _repository;

    public GetPaymentHandler(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetPaymentCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var payment = await _repository.GetByIdWithOrderAsync(new Guid(command.PaymentId));

        // Query personalized coffee exist
        if (payment is null)
        {
            AddNotification(command.PaymentId, "Pagamento não cadastrado");
            return new CommandResult(false, Notifications);
        }

        return new CommandResult(true, new PaymentCommandResult(payment));
    }
}