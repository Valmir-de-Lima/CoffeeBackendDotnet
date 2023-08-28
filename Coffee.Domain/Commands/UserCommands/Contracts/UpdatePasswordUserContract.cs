using Flunt.Validations;

namespace Coffee.Domain.Commands.UserCommands.Contracts;

public class UpdatePasswordUserContract : Contract<UpdatePasswordUserCommand>
{
    public UpdatePasswordUserContract(string oldPassword, string newPassword)
    {
        Requires()
                .IsNotEmpty(oldPassword, "oldPassword", "Informe a senha antiga")
                .IsNotEmpty(newPassword, "newPassword", "Informe a senha nova");
    }
}
