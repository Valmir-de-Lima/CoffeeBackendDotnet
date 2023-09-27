using Coffee.Domain.Models.Orders.Items.Ingredients.Contracts;

namespace Coffee.Domain.Models.Orders.Items.Ingredients;

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
    public Item Item { get; private set; } = new Item();

    public void Update(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;
    }
}