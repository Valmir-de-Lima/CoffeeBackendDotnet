using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.BasketCommands;

public class CreateBasketCommand : Command, ICommand
{
    public string CustomerId { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(CustomerId)
        );
    }
}