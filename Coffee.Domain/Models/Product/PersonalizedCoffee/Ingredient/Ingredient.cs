using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient.Contracts;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;

public class Ingredient : Model
{
    public Ingredient()
    {

    }
    public Ingredient(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;

        // Design by contracts
        AddNotifications(
            new CreateIngredientContract(this)
        );
    }

    public string Description { get; private set; } = "";
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    public void Update(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;
    }
}