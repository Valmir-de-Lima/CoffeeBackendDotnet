using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Models.Product;
using Coffee.Domain.Repositories.Interfaces;


namespace Coffee.Domain.Handlers.BasketHandlers;

public class AddProductBasketHandler : Handler, IHandler<AddProductBasketCommand>
{

    private readonly IBasketRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IPersonalizedCoffeeRepository _personalizedCoffeeRepository;
    private readonly IPastryRepository _pastryRepository;


    public AddProductBasketHandler(
        IBasketRepository repository,
        IProductRepository productRepository,
        IPersonalizedCoffeeRepository personalizedCoffeeRepository,
        IPastryRepository pastryRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
        _personalizedCoffeeRepository = personalizedCoffeeRepository;
        _pastryRepository = pastryRepository;
    }

    public async Task<ICommandResult> HandleAsync(AddProductBasketCommand command)
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

        bool isCoffee;
        bool.TryParse(command.IsCoffee, out isCoffee);

        var product = new Product();
        product.Basket = basket;
        product.CustomerId = basket.CustomerId;

        if (isCoffee)
        {
            var coffee = await _personalizedCoffeeRepository.GetByIdWithIngredientAsync(new Guid(command.ProductId));
            // Query coffee exist
            if (coffee is null)
            {
                AddNotification(command.ProductId, "Café personalizado não selecionado");
                return new CommandResult(false, Notifications);
            }

            product.ProductId = coffee.CoffeId;
            product.Description = coffee.DescriptionCoffe;
            product.UnitPrice = coffee.TotalPrice;
            product.Quantity = 1;
            product.TotalPrice = coffee.TotalPrice;
            product.IsCoffee = isCoffee;
            product.Ingredients = coffee.Ingredients;
        }
        else
        {
            var pastry = await _pastryRepository.GetByIdAsync(new Guid(command.ProductId));
            // Query coffee exist
            if (pastry is null)
            {
                AddNotification(command.ProductId, "Acompanhamento não selecionado");
                return new CommandResult(false, Notifications);
            }

            product.ProductId = pastry.Id;
            product.Description = pastry.Description;
            product.UnitPrice = pastry.Price;
            product.Quantity = 1;
            product.TotalPrice = pastry.Price;
            product.IsCoffee = isCoffee;
        }

        // Query ingredient selected
        if (basket.SelectedProduct(product))
        {
            AddNotification(command.ProductId, "Produto já selecionado");
            return new CommandResult(false, Notifications);
        }

        // Save database
        await _productRepository.CreateAsync(product);

        // update model
        basket.AddProduct();
        _repository.Update(basket);

        return new CommandResult(true, new BasketCommandResult(basket));
    }
}