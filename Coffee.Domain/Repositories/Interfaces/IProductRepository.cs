using Coffee.Domain.Models.Product;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
    Task<dynamic> GetByIdWithIngredientAsync(Guid id);
}
