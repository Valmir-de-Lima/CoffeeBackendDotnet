using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Models.Product;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.ProductsRepository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly CoffeeDataContext _context;

    public ProductRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Products
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<ProductCommandResult>(
                await _context.Products
                            .AsNoTracking()
                            .Select(x => new ProductCommandResult(x))
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
        var product = await _context.Products
                        .Include(x => x.Ingredients)
                        .FirstOrDefaultAsync(x => x.Id == id);
        return product ?? null!;
    }

    public async Task<Product?> GetByDescriptionAsync(string description)
    {
        return await _context.Products.FirstOrDefaultAsync(
            ProductQueries.GetByDescriptionCoffe(description)
        );
    }
}