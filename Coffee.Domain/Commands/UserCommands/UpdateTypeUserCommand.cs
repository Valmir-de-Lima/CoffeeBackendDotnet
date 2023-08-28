using Coffee.Domain.Enums;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands.Contracts;

namespace Coffee.Domain.Commands.UserCommands;

public class UpdateTypeUserCommand : Command, ICommand
{
    public string Email { get; set; } = "";
    public int Type { get; set; }

    public void Validate()
    {
        var employeeEmail = new Email(Email);
        AddNotifications(
            new UpdateTypeUserContract((EType)Type),
            employeeEmail
        );
    }
}