using Coffee.Domain.Enums;
using Coffee.Domain.ValueObjects;
using Coffee.Domain.Models.User.Contracts;

namespace Coffee.Domain.Models.User;

public class User : Model
{
    public User()
    {

    }
    public User(string name, Email email, Password passwordHash, EType type)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Link = email.Address.Replace("@", "-").Replace(".", "-");
        Type = type;
        CreatedAt = DateTime.UtcNow.AddHours(BRAZILIAN_UCT);
        Active = false;

        // Design by contracts
        AddNotifications(
            new CreateUserContract(this),
            Email,
            PasswordHash
        );
    }

    public string Name { get; private set; } = "";
    public Email Email { get; private set; } = new("");
    public Password PasswordHash { get; private set; } = new("");
    public string RecoveryPasswordHash { get; private set; } = "";
    public string Link { get; private set; } = "";
    public EType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastLogin { get; private set; }
    public bool Active { get; private set; }

    public void UpdateType(EType type)
    {
        Type = type;
        AddNotifications(
            new CreateUserContract(this)
        );
    }

    public void UpdateName(string name)
    {
        Name = name;
        AddNotifications(
            new CreateUserContract(this)
        );
    }

    public void UpdateActive(bool active)
    {
        Active = active;
        AddNotifications(
            new CreateUserContract(this)
        );
    }

    public void UpdatePassword(Password passwordHash)
    {
        PasswordHash = passwordHash;
        AddNotifications(
            PasswordHash
        );
    }
    public void UpdateRecoveryPassword(string recoveryPasswordHash)
    {
        RecoveryPasswordHash = recoveryPasswordHash;
        AddNotifications(
            new CreateUserContract(this)
        );
    }

    public void UpdateLastLogin()
    {
        LastLogin = DateTime.UtcNow.AddHours(BRAZILIAN_UCT); ;
        AddNotifications(
            new CreateUserContract(this)
        );
    }

    public bool VerifyPassword(string providedPassword)
    {
        return PasswordHash.VerifyPassword(providedPassword);
    }

    public bool VerifyRecoveryPassword(string providedPassword)
    {
        return Password.VerifyPassword(RecoveryPasswordHash, providedPassword);
    }

}