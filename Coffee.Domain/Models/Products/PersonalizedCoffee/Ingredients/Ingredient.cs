using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients.Contracts;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;

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
    public Product Product { get; private set; } = new Product();
    public IList<PersonalizedCoffee> PersonalizedCoffees { get; set; } = new List<PersonalizedCoffee>();

    public void Update(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;
    }
}