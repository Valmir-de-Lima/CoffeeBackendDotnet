using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class UpdatePersonalizedCoffeeCommand : Command, ICommand
{
    public string PersonalizedCoffeeId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string CoffeId { get; set; } = "";
    public string DescriptionCoffe { get; set; } = "";
    public string PriceCoffe { get; set; } = "";

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