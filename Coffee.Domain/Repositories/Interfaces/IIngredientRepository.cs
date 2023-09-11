using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IIngredientRepository : IRepository<Ingredient>
{
    Task<Ingredient?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
