using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;

public class VerifyIdPersonalizedCoffeeContract : Contract<PersonalizedCoffee>
{
    public VerifyIdPersonalizedCoffeeContract(string id)
    {
        Requires()
                .IsNotNullOrEmpty(id.ToString(), id.ToString(), "Identificação requerida")
                .Matches(id.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Valor inválido");
    }
}
