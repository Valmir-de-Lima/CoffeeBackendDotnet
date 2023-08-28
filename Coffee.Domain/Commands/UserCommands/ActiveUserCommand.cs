using Coffee.Domain.Commands.UserCommands.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.UserCommands;

public class ActiveUserCommand : Command, ICommand
{
    public ActiveUserCommand(string id)
    {
        Id = id;
    }
    public string Id { get; set; } = "";

    public void Validate()
    {
        AddNotifications(
            new ActiveUserContract(Id)
        );
    }
}