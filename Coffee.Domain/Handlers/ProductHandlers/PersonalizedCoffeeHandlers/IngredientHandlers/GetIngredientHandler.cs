using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;

public class GetIngredientHandler : Handler, IHandler<GetIngredientCommand>
{

    private readonly IIngredientRepository _repository;

    public GetIngredientHandler(IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(GetIngredientCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var ingredient = await _repository.GetByDescriptionAsync(command.Description);

        // Query ingredient exist
        if (ingredient is null)
        {
            AddNotification(command.Description, "Ingrediente não cadastrado");
            return new CommandResult(false, Notifications);
        }

        return new CommandResult(true, new IngredientCommandResult(ingredient));
    }
}