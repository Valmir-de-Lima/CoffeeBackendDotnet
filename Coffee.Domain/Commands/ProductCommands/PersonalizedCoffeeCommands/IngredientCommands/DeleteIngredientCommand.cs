using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient.Contracts;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.ValueObjects;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;

public class DeleteIngredientCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public string Password { get; set; } = "";
    public void Validate()
    {
        var password = new Password(Password);
        AddNotifications(new CreateIngredientContract(
            new Ingredient(Description, 1, true)
        ),
        password);
    }
}