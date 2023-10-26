using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.OrderCommands;

public class GetOrderCommand : Command, ICommand
{
    public string OrderId { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(OrderId)
        );
    }
}