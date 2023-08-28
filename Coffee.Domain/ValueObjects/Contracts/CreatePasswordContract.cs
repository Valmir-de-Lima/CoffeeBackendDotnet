using Flunt.Validations;

namespace Coffee.Domain.ValueObjects.Contracts;

public class CreatePasswordContract : Contract<Password>
{
    public CreatePasswordContract(string password)
    {
        Requires()
        .Matches(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", "Password", "Password inválido. No mínimo deve conter: uma letra maiúscula, uma letra minúscula, um caracter especial, um dígito e 8 caracteres.");
    }
}
