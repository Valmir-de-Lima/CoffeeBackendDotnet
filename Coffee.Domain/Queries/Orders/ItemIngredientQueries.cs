using Coffee.Domain.Models.Orders.Items.Ingredients;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries.Orders;

public static class ItemIngredientQueries
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