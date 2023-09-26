using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.BasketCommands;

public class DeleteBasketCommand : Command, ICommand
{
    public string BasketId { get; set; } = "";
    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(BasketId)
        );
    }
}