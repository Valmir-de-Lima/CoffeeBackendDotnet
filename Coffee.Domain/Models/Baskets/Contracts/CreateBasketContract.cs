using Flunt.Validations;

namespace Coffee.Domain.Models.Baskets.Contracts;

public class CreateBasketContract : Contract<Basket>
{
    public CreateBasketContract(Basket basket)
    {
        Requires()
                .IsNotNullOrEmpty(basket.CustomerId.ToString(), basket.CustomerId.ToString(), "Identificação requerida")
                .Matches(basket.CustomerId.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Valor inválido");
    }
}
