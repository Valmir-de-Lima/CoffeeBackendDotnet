using Flunt.Validations;

namespace Coffee.Domain.Commands.UserCommands.Contracts;

public class UpdateUserContract : Contract<UpdateUserCommand>
{
    public UpdateUserContract(string name)
    {
        Requires()
                .IsGreaterOrEqualsThan(name.Replace(" ", ""), 3, name, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(name, 40, name, "O nome deve conter no maximo 40 caracteres");
    }
}
