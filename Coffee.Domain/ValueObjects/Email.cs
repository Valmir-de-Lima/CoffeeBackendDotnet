using Coffee.Domain.ValueObjects.Contracts;

namespace Coffee.Domain.ValueObjects;
public class Email : ValueObject
{
    public Email()
    {

    }
    public Email(string address)
    {
        Address = address;

        //Design by contracts
        AddNotifications(
            new CreateEmailContract(this)
        );
    }

    // É necessário o id do Usuario para o EF conectar Email -> User
    public Guid UserId { get; private set; } = new Guid();

    public string Address { get; private set; } = "";
}
