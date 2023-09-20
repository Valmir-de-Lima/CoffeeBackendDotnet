using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;

public class CreatePersonalizedCoffeeContract : Contract<PersonalizedCoffee>
{
    public CreatePersonalizedCoffeeContract(PersonalizedCoffee personalizedCoffee)
    {
        Requires()
                .IsNotNullOrEmpty(personalizedCoffee.CustomerId.ToString(), personalizedCoffee.CustomerId.ToString(), "Identificação do cliente requerida")
                .Matches(personalizedCoffee.CustomerId.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Indentificacao inválida do cliente")
                .IsNotNullOrEmpty(personalizedCoffee.CoffeId.ToString(), personalizedCoffee.CoffeId.ToString(), "Identificação do café requerida")
                .Matches(personalizedCoffee.CoffeId.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Indentificacao inválida do café")
                .IsGreaterOrEqualsThan(personalizedCoffee.DescriptionCoffe.Replace(" ", ""), 3, personalizedCoffee.DescriptionCoffe, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(personalizedCoffee.DescriptionCoffe, 40, personalizedCoffee.DescriptionCoffe, "O nome deve conter no maximo 40 caracteres")
                .IsGreaterThan(personalizedCoffee.PriceCoffe, 0, personalizedCoffee.PriceCoffe.ToString(), "O valor do produto deve ser maior do que zero");
    }
}
