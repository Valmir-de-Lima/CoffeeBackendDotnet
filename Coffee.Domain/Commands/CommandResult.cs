using Coffee.Domain.Commands.Interfaces;

namespace Coffee.Domain.Commands;
public class CommandResult : ICommandResult
{
    public CommandResult(bool success, object? data)
    {
        Success = success;
        Data = data;
    }

    public CommandResult(bool success)
    {
        Success = success;
        Data = new Object();
    }

    public bool Success { get; set; }
    public object? Data { get; set; }
}
