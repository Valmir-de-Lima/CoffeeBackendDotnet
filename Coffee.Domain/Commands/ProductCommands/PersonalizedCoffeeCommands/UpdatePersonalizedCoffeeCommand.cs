using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class UpdatePersonalizedCoffeeCommand : Command, ICommand
{
    public string PersonalizedCoffeeId { get; } = "";
    public string CustomerId { get; } = "";
    public string CoffeId { get; } = "";
    public string DescriptionCoffe { get; private set; } = "";
    public string PriceCoffe { get; private set; } = "";

    public void Validate()
    {
        decimal priceCoffe;
        decimal.TryParse(PriceCoffe, out priceCoffe);
        AddNotifications(
            new CreatePersonalizedCoffeeContract(
                new PersonalizedCoffee(new Guid(CustomerId), new Guid(CoffeId), DescriptionCoffe, priceCoffe)
            ),
            new VerifyIdPersonalizedCoffeeContract(PersonalizedCoffeeId)
        );
    }
}