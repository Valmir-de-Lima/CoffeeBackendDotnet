using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Models.User;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.ValueObjects;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.UsersRepository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CoffeeDataContext _context;

    public UserRepository(CoffeeDataContext context) :
    base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsEmailAsync(Email email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(UserQueries.ExistsEmail(email));
        return (user != null);
    }

    public async Task<dynamic> GetAllAsync(int skip = 0, int take = 25)
    {
        var count = await _context.Users
                            .AsNoTracking()
                            .CountAsync();
        var list = new List<UserCommandResult>(
                await _context.Users
                            .AsNoTracking()
                            .Select(x => new UserCommandResult(x))
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

    public async Task<User?> GetByEmailAsync(Email email)
    {
        return await _context.Users.FirstOrDefaultAsync(UserQueries.GetByEmail(email));
    }

    public async Task<User?> GetByLinkAsync(string link)
    {
        return await _context.Users.FirstOrDefaultAsync(UserQueries.GetByLink(link));
    }
}