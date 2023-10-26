using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.OrderCommands.ItemCommands.IngredientCommands;
using Coffee.Domain.Models.Orders.Items.Ingredients;
using Coffee.Domain.Queries.Orders;
using Coffee.Domain.Repositories.Interfaces.Orders;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.OrdersRepository.ItemsRepository.ItemsIngredientRepository;

public class ItemIngredientRepository : Repository<Ingredient>, IIngredientRepository
{
    private readonly CoffeeDataContext _context;

    public ItemIngredientRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.ItemIngredientes
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<IngredientCommandResult>(
                await _context.ItemIngredientes
                            .AsNoTracking()
                            .Select(x => new IngredientCommandResult(x))
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

    public async Task<Ingredient?> GetByDescriptionAsync(string description)
    {
        return await _context.ItemIngredientes.FirstOrDefaultAsync(ItemIngredientQueries.GetByDescription(description));
    }
}