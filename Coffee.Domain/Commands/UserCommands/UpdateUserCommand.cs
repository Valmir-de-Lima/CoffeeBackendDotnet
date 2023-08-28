using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.UserCommands.Contracts;

namespace Coffee.Domain.Commands.UserCommands;

public class UpdateUserCommand : Command, ICommand
{
    public string Name { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new UpdateUserContract(Name)
        );
    }
}