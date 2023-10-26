using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.PaymentCommands;

public class GetPaymentCommand : Command, ICommand
{
    public string PaymentId { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new VerifyIdPersonalizedCoffeeContract(PaymentId)
        );
    }
}