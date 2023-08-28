using Flunt.Validations;

namespace Coffee.Domain.Commands.UserCommands.Contracts;

public class UpdateRecoveryPasswordUserContract : Contract<UpdateRecoveryPasswordUserCommand>
{
    public UpdateRecoveryPasswordUserContract(string id, string password, string recoveryPassword)
    {
        Requires()
                .IsNotNullOrEmpty(id.ToString(), id.ToString(), "Campo requerido")
                .IsNotNullOrEmpty(password, password, "Campo requerido")
                .IsNotNullOrEmpty(recoveryPassword.ToString(), recoveryPassword.ToString(), "Campo requerido")
                .Matches(id.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Valor inválido")
                .Matches(recoveryPassword.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "recovery", "Valor inválido");
    }
}
