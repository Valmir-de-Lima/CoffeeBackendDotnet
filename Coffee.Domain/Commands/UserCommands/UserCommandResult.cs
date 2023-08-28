using Coffee.Domain.Models.User;

namespace Coffee.Domain.Commands.UserCommands;

public class UserCommandResult
{
    public UserCommandResult(User user)
    {
        Id = user.Id.ToString();
        Name = user.Name;
        Email = user.Email.Address;
        Link = user.Link;
        CreatedAt = user.CreatedAt.ToString();
        LastLogin = user.LastLogin.ToString();
        Type = user.Type.ToString();
        Active = user.Active.ToString();
    }

    public string Id { get; private set; } = "";
    public string Name { get; private set; } = "";
    public string Email { get; private set; } = new("");
    public string Link { get; private set; } = "";
    public string CreatedAt { get; private set; } = "";
    public string LastLogin { get; private set; } = "";
    public string Type { get; private set; } = "";
    public string Active { get; private set; } = "";
}