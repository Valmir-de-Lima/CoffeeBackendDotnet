using Coffee.Domain.Enums;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.UserCommands;

public class CreateManagerCommand : CreateUserCommand, ICommand
{
    public EType Type { get; private set; } = EType.Manager;
    public bool Active { get; private set; } = true;
}