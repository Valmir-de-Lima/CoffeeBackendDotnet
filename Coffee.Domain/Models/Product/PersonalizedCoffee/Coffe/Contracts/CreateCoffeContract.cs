using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe.Contracts;

public class CreateCoffeContract : Contract<Coffe>
{
    public CreateCoffeContract(Coffe coffe)
    {
        Requires()
                .IsGreaterOrEqualsThan(coffe.Description.Replace(" ", ""), 3, coffe.Description, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(coffe.Description, 40, coffe.Description, "O nome deve conter no maximo 40 caracteres")
                .IsNotNullOrEmpty(coffe.Active.ToString(), coffe.Active.ToString(), "O valor de ativação é requerido")
                .IsGreaterThan(coffe.Price, 0, coffe.Price.ToString(), "O valor do café deve ser maior do que zero");
    }
}
