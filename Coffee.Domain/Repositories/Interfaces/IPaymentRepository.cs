using Coffee.Domain.Models.Payments;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByOrderIdAsync(Guid orderId);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
    Task<Payment> GetByCustomerIdAsync(Guid customerId);
}
