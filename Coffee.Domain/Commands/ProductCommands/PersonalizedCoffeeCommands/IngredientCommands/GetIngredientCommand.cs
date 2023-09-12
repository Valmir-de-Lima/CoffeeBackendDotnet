using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;

public class GetIngredientCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public void Validate()
    {
        AddNotifications(new CreateIngredientContract(
            new Ingredient(Description, 1, true)
        ));
    }
}