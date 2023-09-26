using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands;

public class CreateProductCommand : Command, ICommand
{
    public string BasketId { get; set; } = "";
    public string ProductId { get; set; } = "";
    public string IsCoffee { get; set; } = "";


    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(BasketId),
            new VerifyIdPersonalizedCoffeeContract(ProductId),
            new VerifyBooleanContract(IsCoffee)
        );
    }
}