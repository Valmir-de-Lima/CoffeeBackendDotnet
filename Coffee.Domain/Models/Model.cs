using Flunt.Notifications;

namespace Coffee.Domain.Models;

public abstract class Model : Notifiable<Notification>
{
    public const double BRAZILIAN_UCT = -3;
    protected Model()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}