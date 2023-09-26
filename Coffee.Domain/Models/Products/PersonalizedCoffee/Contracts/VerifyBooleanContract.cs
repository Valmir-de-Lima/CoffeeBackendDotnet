using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;

public class VerifyBooleanContract : Contract<PersonalizedCoffee>
{
    public VerifyBooleanContract(string isCoffee)
    {
        Requires()
                .IsNotNullOrEmpty(isCoffee.ToString(), isCoffee.ToString(), "Campo requerido");
    }
}
