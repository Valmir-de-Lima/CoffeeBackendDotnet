using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class IngredientQueries
{
    public static Expression<Func<Ingredient, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Ingredient, bool>> GetByDescription(string description)
    {
        return x => x.Description == description;
    }

}