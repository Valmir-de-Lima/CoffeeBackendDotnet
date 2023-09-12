using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.ProductsRepository.PersonalizedCoffees;

public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
{
    private readonly StoreDataContext _context;

    public IngredientRepository(StoreDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Ingredients
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<IngredientCommandResult>(
                await _context.Ingredients
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
        return await _context.Ingredients.FirstOrDefaultAsync(IngredientQueries.GetByDescription(description));
    }
}