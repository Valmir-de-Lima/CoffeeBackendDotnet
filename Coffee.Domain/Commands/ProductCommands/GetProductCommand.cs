using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands;

public class GetProductCommand : Command, ICommand
{
    public string ProductId { get; set; } = "";
    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(ProductId)
        );
    }
}