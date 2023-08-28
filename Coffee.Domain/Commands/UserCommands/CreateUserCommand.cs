using Coffee.Domain.Enums;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Models.User;
using Coffee.Domain.Models.User.Contracts;
using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands.UserCommands;

public class CreateUserCommand : Command, ICommand
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public void Validate()
    {
        var email = new Email(Email);
        var password = new Password(Password);
        AddNotifications(new CreateUserContract(
            new User(Name, email, password, EType.Customer)
        ),
        email,
        password);
    }
}