using Coffee.Domain.ValueObjects.Contracts;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.ValueObjects;

namespace Coffee.Domain.Commands.UserCommands;

public class DeleteUserCommand : Command, ICommand
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public void Validate()
    {
        var password = new Password(Password);
        AddNotifications(new CreateEmailContract(
            new Email(Email)
        ),
        password);
    }
}