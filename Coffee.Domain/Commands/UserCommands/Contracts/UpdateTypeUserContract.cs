using Flunt.Validations;
using Coffee.Domain.Enums;

namespace Coffee.Domain.Commands.UserCommands.Contracts;

public class UpdateTypeUserContract : Contract<UpdateTypeUserCommand>
{
    public UpdateTypeUserContract(EType type)
    {
        Requires()
                .IsBetween((int)type, 0, 3, "user.Type", "Tipo de usuário inválido");
    }
}
