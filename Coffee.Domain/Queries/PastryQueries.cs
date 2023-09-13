using Coffee.Domain.Models.Product.Pastry;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class PastryQueries
{
    public static Expression<Func<Pastry, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Pastry, bool>> GetByDescription(string description)
    {
        return x => x.Description == description;
    }
}