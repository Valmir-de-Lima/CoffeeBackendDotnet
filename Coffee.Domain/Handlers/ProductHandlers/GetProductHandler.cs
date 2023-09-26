using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers;

public class GetProductHandler : Handler, IHandler<GetProductCommand>
{

    private readonly IProductRepository _repository;

    public GetProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetProductCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var product = await _repository.GetByIdWithIngredientAsync(new Guid(command.ProductId));

        // Query personalized coffee exist
        if (product is null)
        {
            AddNotification(command.ProductId, "Produto não cadastrado");
            return new CommandResult(false, Notifications);
        }

        return new CommandResult(true, new ProductCommandResult(product));
    }
}