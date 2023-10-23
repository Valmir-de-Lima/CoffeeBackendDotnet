using Coffee.Domain.Models.Payments;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class PaymentQueries
{
    public static Expression<Func<Payment, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }
    public static Expression<Func<Payment, bool>> GetByOrderId(Guid orderId)
    {
        return x => x.OrderId == orderId;
    }

    public static Expression<Func<Payment, bool>> GetByCustomerId(Guid customerId)
    {
        return x => x.CustomerId == customerId;
    }

}