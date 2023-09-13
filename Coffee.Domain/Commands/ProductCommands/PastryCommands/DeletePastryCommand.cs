using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Models.Product.Pastry.Contracts;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.ValueObjects;

namespace Coffee.Domain.Commands.ProductCommands.PastryCommands;

public class DeletePastryCommand : Command, ICommand
{
    public string Description { get; set; } = "";
    public string Password { get; set; } = "";
    public void Validate()
    {
        var password = new Password(Password);
        AddNotifications(new CreatePastryContract(
            new Pastry(Description, 1, true)
        ),
        password);
    }
}