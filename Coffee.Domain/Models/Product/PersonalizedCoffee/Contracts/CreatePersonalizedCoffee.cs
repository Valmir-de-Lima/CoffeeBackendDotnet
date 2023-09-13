using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;

public class CreatePersonalizedCoffeeContract : Contract<PersonalizedCoffee>
{
    public CreatePersonalizedCoffeeContract(PersonalizedCoffee personalizedCoffee)
    {
        Requires()
                .IsGreaterOrEqualsThan(personalizedCoffee.DescriptionCoffe.Replace(" ", ""), 3, personalizedCoffee.DescriptionCoffe, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(personalizedCoffee.DescriptionCoffe, 40, personalizedCoffee.DescriptionCoffe, "O nome deve conter no maximo 40 caracteres")
                .IsGreaterThan(personalizedCoffee.PriceCoffe, 0, personalizedCoffee.PriceCoffe.ToString(), "O valor do produto deve ser maior do que zero");
    }
}
