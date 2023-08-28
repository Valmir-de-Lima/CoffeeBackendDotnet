using Coffee.Domain.Commands.UserCommands.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.UserCommands;

public class GetUserCommand : Command, ICommand
{
    public GetUserCommand(string link)
    {
        Link = link;
    }
    public string Link { get; set; } = "";

    public void Validate()
    {
        AddNotifications(new GetUserContract(Link));
    }
}