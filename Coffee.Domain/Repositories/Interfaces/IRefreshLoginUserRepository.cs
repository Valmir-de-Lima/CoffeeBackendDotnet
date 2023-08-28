using Coffee.Domain.Models.User;

namespace Coffee.Domain.Repositories.Interfaces;
public interface IRefreshLoginUserRepository
{
    Task CreateAsync(RefreshLoginUser refreshLoginUser);

    void Delete(RefreshLoginUser refreshLoginUser);

    Task<string?> GetByUserNameAsync(string userName);

    Task<RefreshLoginUser?> GetByUserNameAndRefreshTokenAsync(string userName, string refreshToken);
}
