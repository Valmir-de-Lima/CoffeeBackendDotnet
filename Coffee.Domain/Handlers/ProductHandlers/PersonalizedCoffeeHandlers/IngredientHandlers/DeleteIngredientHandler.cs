using Coffee.Domain.Enums;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;

public class DeleteIngredientHandler : Handler, IHandler<DeleteIngredientCommand>
{

    private readonly IIngredientRepository _repository;

    public DeleteIngredientHandler(IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(DeleteIngredientCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var userType = command.GetUserType();

        if ((userType != EType.Manager) && (userType != EType.Barista))
        {
            AddNotification(userType.ToString(), "Informação indisponível");
            return new CommandResult(false, Notifications);
        }

        var ingredient = await _repository.GetByDescriptionAsync(command.Description);

        // Query ingredient exist
        if (ingredient is null)
        {
            AddNotification(command.Description, "Ingrediente não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Delete database
        _repository.Delete(ingredient);

        return new CommandResult(true, new IngredientCommandResult(ingredient));
    }
}