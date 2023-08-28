using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Handlers.Interfaces;
public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> HandleAsync(T command);
}
