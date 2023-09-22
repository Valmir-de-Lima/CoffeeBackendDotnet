using Flunt.Validations;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients.Contracts;

public class UpdateIngredientContract : Contract<Ingredient>
{
    public UpdateIngredientContract(string id)
    {
        Requires()
                .IsNotNullOrEmpty(id.ToString(), id.ToString(), "Identificação requerida")
                .Matches(id.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Valor inválido");
    }
}
