using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.OrderCommands.ItemCommands;
using Coffee.Domain.Models.Orders.Items;
using Coffee.Domain.Queries.Orders;
using Coffee.Domain.Repositories.Interfaces.Orders;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.OrdersRepository.ItemsRepository;

public class ItemRepository : Repository<Item>, IItemRepository
{
    private readonly CoffeeDataContext _context;

    public ItemRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Items
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<ItemCommandResult>(
                await _context.Items
                            .AsNoTracking()
                            .Select(x => new ItemCommandResult(x))
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

    public async Task<dynamic> GetByIdWithIngredientAsync(Guid id)
    {
        var item = await _context.Items
                        .Include(x => x.Ingredients)
                        .FirstOrDefaultAsync(x => x.Id == id);
        return item ?? null!;
    }

    public async Task<Item?> GetByDescriptionAsync(string description)
    {
        return await _context.Items.FirstOrDefaultAsync(
            ItemQueries.GetByDescription(description)
        );
    }
}