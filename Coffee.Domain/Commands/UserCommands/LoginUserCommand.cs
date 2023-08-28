using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.UserCommands;

public class LoginUserCommand : Command, ICommand
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public void Validate()
    {
        var email = new Email(Email);
        var password = new Password(Password);
        AddNotifications(email, password);
    }
}