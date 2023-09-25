using Flunt.Validations;

namespace Coffee.Domain.Models.Baskets.Contracts;

public class CreateBasketContract : Contract<Basket>
{
    public CreateBasketContract(Basket basket)
    {
        Requires()
                .IsGreaterThan(basket.Quantity, -1, basket.Quantity.ToString(), "A quantidade não pode ser um valor negativo")
                .IsGreaterThan(basket.Price, -1, basket.Price.ToString(), "O valor não pode negativo");
    }
}
