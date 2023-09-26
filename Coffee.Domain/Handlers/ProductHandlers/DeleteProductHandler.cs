using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers;

public class DeleteProductHandler : Handler, IHandler<DeleteProductCommand>
{

    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(DeleteProductCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var product = await _repository.GetByIdAsync(new Guid(command.ProductId));

        // Query personalized coffee exist
        if (product is null)
        {
            AddNotification(command.ProductId, "Produto não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Delete database
        _repository.Delete(product);

        return new CommandResult(true, new ProductCommandResult(product));
    }
}