using Coffee.Domain.Models.Product.PersonalizedCoffee.Coffe;

namespace Coffee.Domain.Repositories.Interfaces;
public interface ICoffeRepository : IRepository<Coffe>
{
    Task<Coffe?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
