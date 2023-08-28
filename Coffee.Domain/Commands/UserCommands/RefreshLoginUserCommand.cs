using Coffee.Domain.Models.User;
using Coffee.Domain.Models.User.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.UserCommands;

public class RefreshLoginUserCommand : Command, ICommand
{
    public string Token { get; set; } = "";
    public string RefreshToken { get; set; } = "";

    public void Validate()
    {
        AddNotifications(new RefreshLoginUserContract(
            new RefreshLoginUser(Token, RefreshToken)));
    }
}