using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Models.Product;

namespace Coffee.Domain.Handlers.ProductHandlers;

public class CreateProductHandler : Handler, IHandler<CreateProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IBasketRepository _basketRepository;
    private readonly IPersonalizedCoffeeRepository _personalizedCoffeeRepository;
    private readonly IPastryRepository _pastryRepository;
    private readonly IUserRepository _userRepository;

    public CreateProductHandler(
        IProductRepository repository,
        IBasketRepository basketRepository,
        IPersonalizedCoffeeRepository personalizedCoffeeRepository,
        IPastryRepository pastryRepository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _basketRepository = basketRepository;
        _userRepository = userRepository;
        _personalizedCoffeeRepository = personalizedCoffeeRepository;
        _pastryRepository = pastryRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreateProductCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var basket = await _basketRepository.GetByIdAsync(new Guid(command.BasketId));

        // Query basket exist
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
            var coffee = await _personalizedCoffeeRepository.GetByIdAsync(new Guid(command.ProductId));
            // Query coffee exist
            if (coffee is null)
            {
                AddNotification(command.ProductId, "Café personalizado não cadastrado");
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
                AddNotification(command.ProductId, "Acompanhamento não cadastrado");
                return new CommandResult(false, Notifications);
            }

            product.ProductId = pastry.Id;
            product.Description = pastry.Description;
            product.UnitPrice = pastry.Price;
            product.Quantity = 1;
            product.TotalPrice = pastry.Price;
            product.IsCoffee = isCoffee;
        }

        // Save database
        await _repository.CreateAsync(product);

        return new CommandResult(true, new ProductCommandResult(product));
    }
}