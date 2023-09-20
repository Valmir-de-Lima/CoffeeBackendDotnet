using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class GetPersonalizedCoffeeCommand : Command, ICommand
{
    public string PersonalizedCoffeeId { get; } = "";
    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(PersonalizedCoffeeId)
        );
    }
}