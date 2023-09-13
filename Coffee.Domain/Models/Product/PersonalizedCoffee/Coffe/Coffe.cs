using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe.Contracts;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;

public class Coffe : Model
{
    public Coffe()
    {

    }
    public Coffe(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;

        // Design by contracts
        AddNotifications(
            new CreateCoffeeContract(this)
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