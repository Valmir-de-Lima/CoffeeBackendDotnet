using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Models.Product.Pastry.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PastryCommands;

public class GetPastryCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public void Validate()
    {
        AddNotifications(new CreatePastryContract(
            new Pastry(Description, 1, true)
        ));
    }
}