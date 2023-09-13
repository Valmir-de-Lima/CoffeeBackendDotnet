using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe.Contracts;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.ValueObjects;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;

public class DeleteCoffeCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public string Password { get; set; } = "";
    public void Validate()
    {
        var password = new Password(Password);
        AddNotifications(new CreateCoffeContract(
            new Coffe(Description, 1, true)
        ),
        password);
    }
}