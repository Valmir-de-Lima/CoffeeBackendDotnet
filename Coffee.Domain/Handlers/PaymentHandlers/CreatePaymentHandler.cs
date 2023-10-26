using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.PaymentCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Repositories.Interfaces.Orders;
using Coffee.Domain.Models.Payments;
using Coffee.Domain.Models.Orders;
using Coffee.Domain.Models.Orders.Items;
using Coffee.Domain.Models.Orders.Items.Ingredients;
using Microsoft.VisualBasic;

namespace Coffee.Domain.Handlers.PaymentHandlers;

public class CreatePaymentHandler : Handler, IHandler<CreatePaymentCommand>
{
    private readonly IPaymentRepository _repository;
    private readonly IBasketRepository _basketRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public CreatePaymentHandler(
        IPaymentRepository repository,
        IBasketRepository basketRepository,
        IOrderRepository orderRepository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _basketRepository = basketRepository;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePaymentCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var basket = await _basketRepository.GetByIdWithProductsAsync(new Guid(command.BasketId));

        // Query basket exist
        if (basket is null)
        {
            AddNotification(command.BasketId, "Cesta de compras não cadastrada");
            return new CommandResult(false, Notifications);
        }

        var order = new Order();

        // Create Payment
        var payment = new Payment();
        payment.CustomerId = basket.CustomerId;
        payment.Type = command.Type;
        payment.Price = basket.Price;
        payment.Date = DateTime.Today;
        payment.OrderId = order.Id;
        // Save Payment
        await _repository.CreateAsync(payment);

        // Create Order
        order.CustomerId = basket.CustomerId;
        order.Quantity = basket.Quantity;
        order.Price = basket.Price;
        order.Status = "";
        order.Adress = command.Adress;
        foreach (var product in basket.Products)
        {
            var item = new Item();
            item.Id = product.Id;
            item.OrderId = order.Id;
            item.CustomerId = basket.CustomerId;
            item.ItemId = product.Id;
            item.Description = product.Description;
            item.UnitPrice = product.UnitPrice;
            item.Quantity = product.Quantity;
            item.TotalPrice = product.TotalPrice;
            item.IsCoffee = product.IsCoffee;

            foreach (var ingredient in product.Ingredients)
            {
                var itemIngredient = new Ingredient();
                itemIngredient.Id = ingredient.Id;
                itemIngredient.Update(ingredient.Description, ingredient.Price, ingredient.Active);
                item.Ingredients.Add(itemIngredient);
            }
            order.Items.Add(item);
        }
        // Save Order
        await _orderRepository.CreateAsync(order);

        return new CommandResult(true, new PaymentCommandResult(payment));
    }
}