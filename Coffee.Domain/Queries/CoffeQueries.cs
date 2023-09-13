using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class CoffeQueries
{
    public static Expression<Func<Coffe, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<Coffe, bool>> GetByDescription(string description)
    {
        return x => x.Description == description;
    }
}