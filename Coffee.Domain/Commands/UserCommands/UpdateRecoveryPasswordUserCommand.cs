using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands.Contracts;

namespace Coffee.Domain.Commands.UserCommands;

public class UpdateRecoveryPasswordUserCommand : Command, ICommand
{
    public string Id { get; set; } = "";
    public string Password { get; set; } = "";

    public string RecoveryPassword { get; set; } = "";

    public void Validate()
    {
        var password = new Password(Password);
        AddNotifications(
            new UpdateRecoveryPasswordUserContract(Id, Password, RecoveryPassword),
            password
        );
    }
}