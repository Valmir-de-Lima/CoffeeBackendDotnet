using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;

public class UpdateIngredientCommand : Command, ICommand
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
        AddNotifications(new CreateIngredientContract(
            new Ingredient(Description, priceDecimal, activeBool)
        ));
    }
}