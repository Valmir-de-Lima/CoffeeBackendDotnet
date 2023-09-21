using Coffee.Domain.Models.Product.PersonalizedCoffee.IngredientsSelected.Contracts;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.IngredientsSelected;

public class IngredientSelected : Model
{
    public IngredientSelected()
    {

    }
    public IngredientSelected(Guid id, string description, decimal price, bool active)
    {
        Id = id;
        Description = description;
        Price = price;
        Active = active;

        // Design by contracts
        AddNotifications(
            new CreateIngredientSelectedContract(this)
        );
    }

    public Guid Id { get; set; }
    public string Description { get; private set; } = "";
    public decimal Price { get; private set; }
    public bool Active { get; private set; }
    public Guid PersonalizedCoffeeId { get; set; }
    public PersonalizedCoffee PersonalizedCoffee { get; set; } = new PersonalizedCoffee();

    public void Update(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;
    }
}