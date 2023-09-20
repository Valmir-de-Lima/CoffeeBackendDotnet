using Coffee.Domain.Models.Product.PersonalizedCoffee;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IPersonalizedCoffeeRepository : IRepository<PersonalizedCoffee>
{
    Task<PersonalizedCoffee?> GetByDescriptionCoffeAsync(string descriptionCoffe);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
