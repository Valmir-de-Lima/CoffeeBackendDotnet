using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class RemoveIngredientPersonalizedCoffeeCommand : Command, ICommand
{
    public string CustomerId { get; set; } = "";
    public string CoffeId { get; set; } = "";
    public string IngredientId { get; set; } = "";
    public void Validate()
    {
        AddNotifications(
            new UpdateIngredientContract(CustomerId),
            new UpdateIngredientContract(CoffeId),
            new UpdateIngredientContract(IngredientId)
        );
    }
}