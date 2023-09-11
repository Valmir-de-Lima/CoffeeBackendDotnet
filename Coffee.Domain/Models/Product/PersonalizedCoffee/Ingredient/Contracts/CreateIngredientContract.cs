using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient.Contracts;

public class CreateIngredientContract : Contract<Ingredient>
{
    public CreateIngredientContract(Ingredient ingredient)
    {
        Requires()
                .IsGreaterOrEqualsThan(ingredient.Description.Replace(" ", ""), 3, ingredient.Description, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(ingredient.Description, 40, ingredient.Description, "O nome deve conter no maximo 40 caracteres")
                .IsNotNullOrEmpty(ingredient.Active.ToString(), ingredient.Active.ToString(), "O valor de ativação é requerido")
                .IsGreaterThan(ingredient.Price, 0, ingredient.Price.ToString(), "O valor do ingrediente deve ser maior do que zero");
    }
}
