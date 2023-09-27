using Coffee.Domain.Models.Orders;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries.Orders;

public static class OrderQueries
{
    public static Expression<Func<Order, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Order, bool>> GetByCustomerId(Guid id)
    {
        return x => x.CustomerId == id;
    }
}