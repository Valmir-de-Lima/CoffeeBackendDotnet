using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;


namespace Coffee.Domain.Handlers.BasketHandlers;

public class RemoveProductBasketHandler : Handler, IHandler<RemoveProductBasketCommand>
{

    private readonly IBasketRepository _repository;
    private readonly IProductRepository _productRepository;

    public RemoveProductBasketHandler(IBasketRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ICommandResult> HandleAsync(RemoveProductBasketCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var basket = await _repository.GetByIdWithProductsAsync(new Guid(command.BasketId));

        // Query personalized coffee exist
        if (basket is null)
        {
            AddNotification(command.BasketId, "Cesta de compras não cadastrada");
            return new CommandResult(false, Notifications);
        }

        var product = basket.SelectedProduct(new Guid(command.ProductId));

        // Query ingredient exist
        if (product is null)
        {
            AddNotification(command.ProductId, "Produto não selecionado");
            return new CommandResult(false, Notifications);
        }

        // Remove
        _productRepository.Delete(product);

        basket.Quantity = 0;
        basket.Price = 0;
        foreach (var prod in basket.Products)
        {
            basket.Price = basket.Price + prod.TotalPrice;
            basket.Quantity = basket.Quantity + prod.Quantity;
        }

        // Save database
        _repository.Update(basket);

        return new CommandResult(true, new BasketCommandResult(basket));
    }
}