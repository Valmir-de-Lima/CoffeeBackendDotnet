using Coffee.Domain.Models.Orders.Items;

namespace Coffee.Domain.Repositories.Interfaces.Orders;
public interface IItemRepository : IRepository<Item>
{
    Task<Item?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
    Task<dynamic> GetByIdWithIngredientAsync(Guid id);
}
