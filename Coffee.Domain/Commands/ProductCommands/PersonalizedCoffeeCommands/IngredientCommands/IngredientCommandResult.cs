using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;

public class IngredientCommandResult
{
    public IngredientCommandResult(Ingredient ingredient)
    {
        Id = ingredient.Id.ToString();
        Description = ingredient.Description;
        Price = ingredient.Price.ToString();
        Active = ingredient.Active.ToString();
    }

    public string Id { get; private set; } = "";
    public string Description { get; private set; } = "";
    public string Price { get; private set; } = "";
    public string Active { get; private set; } = "";
}