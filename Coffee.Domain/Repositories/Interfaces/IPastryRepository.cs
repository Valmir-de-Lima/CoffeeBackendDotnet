using Coffee.Domain.Models.Product.Pastry;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IPastryRepository : IRepository<Pastry>
{
    Task<Pastry?> GetByDescriptionAsync(string description);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
