using Coffee.Domain.Models.User.Contracts;

namespace Coffee.Domain.Models.User;

public class RefreshLoginUser : Model
{
    public RefreshLoginUser()
    {

    }
    public RefreshLoginUser(string userName, string refreshToken)
    {
        UserName = userName;
        RefreshToken = refreshToken;

        // Design by contracts
        AddNotifications(
            new RefreshLoginUserContract(this)
        );
    }

    public string UserName { get; private set; } = "";
    public string RefreshToken { get; private set; } = "";
}