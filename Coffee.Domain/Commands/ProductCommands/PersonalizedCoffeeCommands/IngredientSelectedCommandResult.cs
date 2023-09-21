using Coffee.Domain.Models.Product.PersonalizedCoffee.IngredientsSelected;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class IngredientSelectedCommandResult
{
    public IngredientSelectedCommandResult(IngredientSelected ingredient)
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