using Flunt.Validations;

namespace Coffee.Domain.Models.Product.Pastry.Contracts;

public class CreatePastryContract : Contract<Pastry>
{
    public CreatePastryContract(Pastry ingredient)
    {
        Requires()
                .IsGreaterOrEqualsThan(ingredient.Description.Replace(" ", ""), 3, ingredient.Description, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(ingredient.Description, 40, ingredient.Description, "O nome deve conter no maximo 40 caracteres")
                .IsNotNullOrEmpty(ingredient.Active.ToString(), ingredient.Active.ToString(), "O valor de ativação é requerido")
                .IsGreaterThan(ingredient.Price, 0, ingredient.Price.ToString(), "O valor do ingrediente deve ser maior do que zero");
    }
}
