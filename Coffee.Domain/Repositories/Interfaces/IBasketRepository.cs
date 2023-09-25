using Coffee.Domain.Models.Baskets;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IBasketRepository : IRepository<Basket>
{
    Task<Basket?> GetByCustomerId(Guid Id);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
    Task<dynamic> GetByIdWithProductsAsync(Guid id);
}
