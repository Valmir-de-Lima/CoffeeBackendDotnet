using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Models.User;
using Coffee.Domain.Queries;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.ValueObjects;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly StoreDataContext _context;

    public UserRepository(StoreDataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
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

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(UserQueries.GetById(id));
    }


    public async Task<User?> GetByLinkAsync(string link)
    {
        return await _context.Users.FirstOrDefaultAsync(UserQueries.GetByLink(link));
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}