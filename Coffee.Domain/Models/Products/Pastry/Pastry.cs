using Coffee.Domain.Models.Product.Pastry.Contracts;

namespace Coffee.Domain.Models.Product.Pastry;

public class Pastry : Model
{
    public Pastry()
    {

    }
    public Pastry(string description, decimal price, bool active)
    {
        Description = description;
        Price = price;
        Active = active;

        // Design by contracts
        AddNotifications(
            new CreatePastryContract(this)
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