using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;


namespace Coffee.Domain.Handlers.BasketHandlers;

public class IncreaseQuantityProductBasketHandler : Handler, IHandler<IncreaseQuantityProductBasketCommand>
{

    private readonly IBasketRepository _repository;
    private readonly IProductRepository _productRepository;

    public IncreaseQuantityProductBasketHandler(IBasketRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ICommandResult> HandleAsync(IncreaseQuantityProductBasketCommand command)
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

        var product = await _productRepository.GetByIdAsync(new Guid(command.ProductId));

        // Query ingredient exist
        if (product is null)
        {
            AddNotification(command.ProductId, "Produto não selecionado");
            return new CommandResult(false, Notifications);
        }

        // Query ingredient selected
        if (!basket.SelectedProduct(product))
        {
            AddNotification(command.ProductId, "Produto não selecionado");
            return new CommandResult(false, Notifications);
        }

        product.Update(1);
        _productRepository.Update(product);

        // update model
        basket.IncreaseQuantityProduc(product);
        // Save database
        _repository.Update(basket);

        return new CommandResult(true, new BasketCommandResult(basket));
    }
}