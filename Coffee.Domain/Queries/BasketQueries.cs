using Coffee.Domain.Models.Baskets;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class BasketQueries
{
    public static Expression<Func<Basket, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Basket, bool>> GetByCustomerId(Guid id)
    {
        return x => x.CustomerId == id;
    }
}