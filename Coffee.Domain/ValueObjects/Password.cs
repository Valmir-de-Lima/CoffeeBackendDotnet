using SecureIdentity.Password;
using Coffee.Domain.ValueObjects.Contracts;

namespace Coffee.Domain.ValueObjects;
public class Password : ValueObject
{
    public Password()
    {

    }
    public Password(string password)
    {
        //Design by contracts
        AddNotifications(
            new CreatePasswordContract(password)
        );

        if (IsValid)
            PasswordHash = PasswordHasher.Hash(password);
    }

    // É necessário o id do Usuario para o EF conectar Password -> User
    public Guid UserId { get; private set; } = new Guid();

    public string PasswordHash { get; private set; } = "";

    public override string ToString()
    {
        return PasswordHash;
    }

    public bool VerifyPassword(string providedPassword)
    {
        return PasswordHasher.Verify(PasswordHash, providedPassword);
    }

    public static bool VerifyPassword(string passwordHash, string providedPassword)
    {
        return PasswordHasher.Verify(passwordHash, providedPassword);
    }
}
