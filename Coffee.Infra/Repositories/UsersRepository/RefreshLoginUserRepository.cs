using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Models.User;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Infra.Data;

namespace Coffee.Infra.Repositories.UsersRepository;

public class RefreshLoginUserRepository : IRefreshLoginUserRepository
{
    private readonly CoffeeDataContext _context;
    private Repository<RefreshLoginUser> _repository;

    public RefreshLoginUserRepository(CoffeeDataContext context)
    {
        _context = context;
        _repository = new Repository<RefreshLoginUser>(context);
    }

    public async Task CreateAsync(RefreshLoginUser refreshLoginUser)
    {
        await _repository.CreateAsync(refreshLoginUser);
    }

    public void Delete(RefreshLoginUser item)
    {
        _repository.Delete(item);
    }

    public async Task<string?> GetByUserNameAsync(string userName)
    {
        var refreshLoginUser = await _context.RefreshLoginUsers.FirstOrDefaultAsync(x => x.UserName == userName);
        if (refreshLoginUser != null)
            return refreshLoginUser.RefreshToken;
        return "";
    }

    public async Task<RefreshLoginUser?> GetByUserNameAndRefreshTokenAsync(string userName, string refreshToken)
    {
        return await _context.RefreshLoginUsers.FirstOrDefaultAsync(x => x.UserName == userName && x.RefreshToken == refreshToken);
    }

}