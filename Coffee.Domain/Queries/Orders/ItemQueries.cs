using Coffee.Domain.Models.Orders.Items;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries.Orders;

public static class ItemQueries
{
    public static Expression<Func<Item, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Item, bool>> GetByDescription(string description)
    {
        return x => x.Description == description;
    }
}