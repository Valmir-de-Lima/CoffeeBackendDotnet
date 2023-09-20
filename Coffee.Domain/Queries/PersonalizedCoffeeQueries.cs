using Coffee.Domain.Models.Product.PersonalizedCoffee;
using System.Linq.Expressions;

namespace Coffee.Domain.Queries;

public static class PersonalizedCoffeeQueries
{
    public static Expression<Func<PersonalizedCoffee, bool>> GetById(Guid id)
    {
        return x => x.Id == id;
    }

    public static Expression<Func<PersonalizedCoffee, bool>> GetByDescriptionCoffe(string descriptionCoffe)
    {
        return x => x.DescriptionCoffe == descriptionCoffe;
    }
}