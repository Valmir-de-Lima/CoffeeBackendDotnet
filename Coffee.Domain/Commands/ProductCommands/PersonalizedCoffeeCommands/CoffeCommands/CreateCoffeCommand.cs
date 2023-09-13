using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;

public class CreateCoffeCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public string Price { get; set; } = "";
    public string Active { get; set; } = "";

    public void Validate()
    {
        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(Price, out priceDecimal);
        bool.TryParse(Active, out activeBool);
        AddNotifications(new CreateCoffeContract(
            new Coffe(Description, priceDecimal, activeBool)
        ));
    }
}