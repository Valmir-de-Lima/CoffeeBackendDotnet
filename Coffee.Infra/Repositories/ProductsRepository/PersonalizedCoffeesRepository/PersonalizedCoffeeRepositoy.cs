using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using System.Security.Cryptography.X509Certificates;

namespace Coffee.Infra.Repositories.ProductsRepository.PersonalizedCoffeesRepository;

public class PersonalizedCoffeeRepository : Repository<PersonalizedCoffee>, IPersonalizedCoffeeRepository
{
    private readonly CoffeeDataContext _context;

    public PersonalizedCoffeeRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.PersonalizedCoffees
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<PersonalizedCoffeeCommandResult>(
                await _context.PersonalizedCoffees
                            .AsNoTracking()
                            .Select(x => new PersonalizedCoffeeCommandResult(x))
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

    public async Task<PersonalizedCoffee?> GetByDescriptionCoffeAsync(string descriptionCoffe)
    {
        return await _context.PersonalizedCoffees.FirstOrDefaultAsync(
            PersonalizedCoffeeQueries.GetByDescriptionCoffe(descriptionCoffe)
        );
    }
}