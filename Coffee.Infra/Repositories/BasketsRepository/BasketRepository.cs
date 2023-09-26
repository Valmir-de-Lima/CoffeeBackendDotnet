using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Models.Baskets;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.BasketsRepository;

public class BasketRepository : Repository<Basket>, IBasketRepository
{
    private readonly CoffeeDataContext _context;

    public BasketRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Baskets
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<BasketCommandResult>(
                await _context.Baskets
                            .AsNoTracking()
                            .Select(x => new BasketCommandResult(x))
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

    public async Task<dynamic> GetByIdWithProductsAsync(Guid id)
    {
        var basket = await _context.Baskets
                        .Include(x => x.Products)
                        .ThenInclude(p => p.Ingredients)
                        .FirstOrDefaultAsync(x => x.Id == id);
        return basket ?? null!;
    }

    public async Task<Basket?> GetByCustomerIdWithProductsAsync(Guid id)
    {
        var basket = await _context.Baskets
                        .Include(x => x.Products)
                        .ThenInclude(p => p.Ingredients)
                        .FirstOrDefaultAsync(x => x.CustomerId == id);
        return basket ?? null!;
    }
}