using Coffee.Domain.Models.Product;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class ProductQueries
{
    public static Expression<Func<Product, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Product, bool>> GetByDescription(string description)
    {
        return x => x.Description == description;
    }
}