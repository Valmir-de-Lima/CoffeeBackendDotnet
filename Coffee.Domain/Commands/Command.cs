using System.Security.Claims;
using Flunt.Notifications;
using Coffee.Domain.Enums;

namespace Coffee.Domain.Commands;
public class Command : Notifiable<Notification>
{
    private ClaimsPrincipal _user = new();
    private string _userName = "";
    private EType _userType;
    private string _urlOfSite = "";
    public ClaimsPrincipal GetUserClaims()
    {
        return _user;
    }
    public void SetUser(ClaimsPrincipal user)
    {
        _user = user;
        if (user.Identity!.Name != null)
            _userName = user.Identity.Name;

        _userType = Enum.Parse<EType>(GetUserRole(user));
    }

    public void SetUserName(string userName)
    {
        _userName = userName;
    }

    public void SetUserType(EType userType)
    {
        _userType = userType;
    }

    public void SetUrlOfSite(string urlOfSite)
    {
        _urlOfSite = urlOfSite;
    }

    public string GetUserName()
    {
        return _userName;
    }
    public EType GetUserType()
    {
        return _userType;
    }

    public string GetUrlOfSite()
    {
        return _urlOfSite;
    }

    private string GetUserRole(ClaimsPrincipal user)
    {
        var roleClaim = user.FindFirst(ClaimTypes.Role);

        if (roleClaim != null)
        {
            return roleClaim.Value;
        }

        return "Customer";
    }
}