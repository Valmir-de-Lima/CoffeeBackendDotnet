using Coffee.Domain.Models.User;
using Coffee.Domain.ValueObjects;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsEmailAsync(Email email);
    Task<User?> GetByEmailAsync(Email email);
    Task<User?> GetByLinkAsync(string link);
    Task<dynamic> GetAllAsync(int skip = 0, int take = 25);
}
