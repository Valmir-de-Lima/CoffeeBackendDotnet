using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Models.Product.Pastry.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PastryCommands;

public class CreatePastryCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public string Price { get; set; } = "";
    public string Active { get; set; } = "";

    public void Validate()
    {
        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(Price, out priceDecimal);
        bool.TryParse(Active, out activeBool);
        AddNotifications(new CreatePastryContract(
            new Pastry(Description, priceDecimal, activeBool)
        ));
    }
}