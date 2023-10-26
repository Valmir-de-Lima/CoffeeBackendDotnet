using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.PaymentCommands;

public class CreatePaymentCommand : Command, ICommand
{
    public string BasketId { get; set; } = "";
    public string Type { get; set; } = "";
    public string Adress { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(BasketId)
        );
    }
}