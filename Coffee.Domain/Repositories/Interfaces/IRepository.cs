using Coffee.Domain.Models;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IRepository<T>
where T : Model
{
    Task CreateAsync(T model);
    Task<T?> GetByIdAsync(Guid id);
    void Update(T model);
    void Delete(T model);
}
