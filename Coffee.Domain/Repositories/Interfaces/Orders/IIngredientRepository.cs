using Coffee.Domain.Models.Orders.Items.Ingredients;

namespace Coffee.Domain.Repositories.Interfaces.Orders;
public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<Ingredient?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
