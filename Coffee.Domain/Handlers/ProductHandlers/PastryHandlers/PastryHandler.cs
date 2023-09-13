using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PastryCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PastryHandlers;

public class PastryHandler : Handler,
    IHandler<CreatePastryCommand>,
    IHandler<GetPastryCommand>,
    IHandler<UpdatePastryCommand>,
    IHandler<DeletePastryCommand>

{
    private readonly IPastryRepository _repository;

    public PastryHandler(IPastryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePastryCommand command)
    {
        return await new CreatePastryHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetPastryCommand command)
    {
        return await new GetPastryHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdatePastryCommand command)
    {
        return await new UpdatePastryHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeletePastryCommand command)
    {
        return await new DeletePastryHandler(_repository).HandleAsync(command);
    }
}