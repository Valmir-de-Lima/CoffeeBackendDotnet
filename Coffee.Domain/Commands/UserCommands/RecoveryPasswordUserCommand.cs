using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.ValueObjects;

namespace Coffee.Domain.Commands.UserCommands;

public class RecoveryPasswordUserCommand : Command, ICommand
{
    public string Email { get; set; } = "";

    public void Validate()
    {
        var email = new Email(Email);
        AddNotifications(email);
    }
}