using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands.Contracts;

namespace Coffee.Domain.Commands.UserCommands;

public class UpdatePasswordUserCommand : Command, ICommand
{
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";

    public void Validate()
    {
        var newPassword = new Password(NewPassword);
        AddNotifications(new UpdatePasswordUserContract(
            OldPassword,
            NewPassword
        ),
        newPassword);
    }
}