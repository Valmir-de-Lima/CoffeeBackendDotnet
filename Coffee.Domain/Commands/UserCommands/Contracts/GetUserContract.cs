using Flunt.Validations;

namespace Coffee.Domain.Commands.UserCommands.Contracts;

public class GetUserContract : Contract<GetUserCommand>
{
    public GetUserContract(string link)
    {
        Requires()
                .IsNotEmpty(link, link, "O link deve ser informado")
                .IsNotNullOrWhiteSpace(link, link, "Link inválido");
    }
}
