using Coffee.Domain.Models;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories;

public class Repository<T> : IRepository<T>
where T : Model
{
    private readonly CoffeeDataContext _context;
    public Repository(CoffeeDataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T model)
    {
        await _context.AddAsync<T>(model);
        _context.SaveChanges();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.FindAsync<T>(id);
    }

    public void Update(T model)
    {
        _context.Update<T>(model);
        _context.SaveChanges();
    }

    public void Delete(T model)
    {
        _context.Remove<T>(model);
        _context.SaveChanges();
    }
}
