using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class RemoveIngredientPersonalizedCoffeeCommand : Command, ICommand
{
    public string PersonalizedCoffeeId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string CoffeId { get; set; } = "";
    public string IngredientId { get; set; } = "";
    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(PersonalizedCoffeeId),
            new VerifyIdPersonalizedCoffeeContract(CustomerId),
            new VerifyIdPersonalizedCoffeeContract(CoffeId),
            new VerifyIdPersonalizedCoffeeContract(IngredientId)
        );
    }
}