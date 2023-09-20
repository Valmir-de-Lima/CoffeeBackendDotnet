using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

public class PersonalizedCoffeeHandler : Handler,
    IHandler<CreatePersonalizedCoffeeCommand>,
    IHandler<GetPersonalizedCoffeeCommand>,
    IHandler<UpdatePersonalizedCoffeeCommand>,
    IHandler<DeletePersonalizedCoffeeCommand>

{
    private readonly IPersonalizedCoffeeRepository _repository;

    public PersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePersonalizedCoffeeCommand command)
    {
        return await new CreatePersonalizedCoffeeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetPersonalizedCoffeeCommand command)
    {
        return await new GetPersonalizedCoffeeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdatePersonalizedCoffeeCommand command)
    {
        return await new UpdatePersonalizedCoffeeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeletePersonalizedCoffeeCommand command)
    {
        return await new DeletePersonalizedCoffeeHandler(_repository).HandleAsync(command);
    }
}