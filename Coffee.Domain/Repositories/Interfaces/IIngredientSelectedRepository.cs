using Coffee.Domain.Models.Product.PersonalizedCoffee.IngredientsSelected;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IIngredientSelectedRepository : IRepository<IngredientSelected>
{
    Task<IngredientSelected?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
