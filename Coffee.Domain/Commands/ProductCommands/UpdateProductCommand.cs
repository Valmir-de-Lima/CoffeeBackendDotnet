using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands;

public class UpdateProductCommand : Command, ICommand
{
    public string CustomerId { get; set; } = "";
    public string ProductId { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(CustomerId),
            new VerifyIdPersonalizedCoffeeContract(ProductId)
        );
    }
}