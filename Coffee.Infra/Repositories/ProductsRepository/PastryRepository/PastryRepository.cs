using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.ProductCommands.PastryCommands;
using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.ProductsRepository.PastryRepository;

public class PastryRepository : Repository<Pastry>, IPastryRepository
{
    private readonly CoffeeDataContext _context;

    public PastryRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Pastrys
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<PastryCommandResult>(
                await _context.Pastrys
                            .AsNoTracking()
                            .Select(x => new PastryCommandResult(x))
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync()
        );
        return new
        {
            count,
            skip,
            take,
            list
        };
    }

    public async Task<Pastry?> GetByDescriptionAsync(string description)
    {
        return await _context.Pastrys.FirstOrDefaultAsync(PastryQueries.GetByDescription(description));
    }
}