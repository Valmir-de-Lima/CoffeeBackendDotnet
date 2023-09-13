using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class GetPersonalizedCoffeeCommand : Command, ICommand
{
    public string CustomerId { get; } = "";
    public string CoffeId { get; } = "";
    public void Validate()
    {
        AddNotifications(new CreatePersonalizedCoffeeContract(
            new PersonalizedCoffee(new Guid(CustomerId), new Guid(CoffeId), "Cafe personalizado", 1)
        ));
    }
}