using Coffee.Domain.Models.Orders;

namespace Coffee.Domain.Repositories.Interfaces.Orders;
public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByCustomerIdWithItemsAsync(Guid id);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
    Task<dynamic> GetByIdWithItemsAsync(Guid id);
}
