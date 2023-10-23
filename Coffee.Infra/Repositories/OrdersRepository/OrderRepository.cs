using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.OrderCommands;
using Coffee.Domain.Models.Orders;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces.Orders;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.OrdersRepository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly CoffeeDataContext _context;

    public OrderRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Orders
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<OrderCommandResult>(
                await _context.Orders
                            .AsNoTracking()
                            .Select(x => new OrderCommandResult(x))
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

    public async Task<dynamic> GetByIdWithItemsAsync(Guid id)
    {
        var order = await _context.Orders
                        .Include(x => x.Items)
                        .ThenInclude(p => p.Ingredients)
                        .FirstOrDefaultAsync(x => x.Id == id);
        return order ?? null!;
    }

    public async Task<Order?> GetByCustomerIdWithItemsAsync(Guid id)
    {
        var order = await _context.Orders
                        .Include(x => x.Items)
                        .ThenInclude(p => p.Ingredients)
                        .FirstOrDefaultAsync(x => x.CustomerId == id);
        return order ?? null!;
    }
}