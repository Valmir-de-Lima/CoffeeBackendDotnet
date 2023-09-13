using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;

public class GetCoffeCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public void Validate()
    {
        AddNotifications(new CreateCoffeContract(
            new Coffe(Description, 1, true)
        ));
    }
}