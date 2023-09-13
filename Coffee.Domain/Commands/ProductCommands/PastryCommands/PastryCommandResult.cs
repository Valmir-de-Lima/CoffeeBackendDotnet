using Coffee.Domain.Models.Product.Pastry;

namespace Coffee.Domain.Commands.ProductCommands.PastryCommands;

public class PastryCommandResult
{
    public PastryCommandResult(Pastry pastry)
    {
        Id = pastry.Id.ToString();
        Description = pastry.Description;
        Price = pastry.Price.ToString();
        Active = pastry.Active.ToString();
    }

    public string Id { get; private set; } = "";
    public string Description { get; private set; } = "";
    public string Price { get; private set; } = "";
    public string Active { get; private set; } = "";
}