using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class CreatePersonalizedCoffeeCommand : Command, ICommand
{
    public string CustomerId { get; set; } = "";
    public string CoffeId { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(CustomerId),
            new VerifyIdPersonalizedCoffeeContract(CoffeId)
        );
    }
}