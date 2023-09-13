using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.CoffeHandlers;

public class CoffeHandler : Handler,
    IHandler<CreateCoffeCommand>,
    IHandler<GetCoffeCommand>,
    IHandler<UpdateCoffeCommand>,
    IHandler<DeleteCoffeCommand>

{
    private readonly ICoffeRepository _repository;

    public CoffeHandler(ICoffeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreateCoffeCommand command)
    {
        return await new CreateCoffeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetCoffeCommand command)
    {
        return await new GetCoffeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdateCoffeCommand command)
    {
        return await new UpdateCoffeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeleteCoffeCommand command)
    {
        return await new DeleteCoffeHandler(_repository).HandleAsync(command);
    }
}