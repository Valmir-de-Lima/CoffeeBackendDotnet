using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;

public class IngredientHandler : Handler,
    IHandler<CreateIngredientCommand>,
    IHandler<GetIngredientCommand>,
    IHandler<UpdateIngredientCommand>,
    IHandler<DeleteIngredientCommand>

{
    private readonly IIngredientRepository _repository;

    public IngredientHandler(IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreateIngredientCommand command)
    {
        return await new CreateIngredientHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetIngredientCommand command)
    {
        return await new GetIngredientHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdateIngredientCommand command)
    {
        return await new UpdateIngredientHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeleteIngredientCommand command)
    {
        return await new DeleteIngredientHandler(_repository).HandleAsync(command);
    }
}