using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;

public class CoffeCommandResult
{
    public CoffeCommandResult(Coffe coffe)
    {
        Id = coffe.Id.ToString();
        Description = coffe.Description;
        Price = coffe.Price.ToString();
        Active = coffe.Active.ToString();
    }

    public string Id { get; private set; } = "";
    public string Description { get; private set; } = "";
    public string Price { get; private set; } = "";
    public string Active { get; private set; } = "";
}