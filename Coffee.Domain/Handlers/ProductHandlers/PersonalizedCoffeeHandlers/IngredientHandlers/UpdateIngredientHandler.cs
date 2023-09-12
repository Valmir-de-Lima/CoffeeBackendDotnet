using Coffee.Domain.Enums;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;

public class UpdateIngredientHandler : Handler, IHandler<UpdateIngredientCommand>
{

    private readonly IIngredientRepository _repository;

    public UpdateIngredientHandler(IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(UpdateIngredientCommand command)
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

        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(command.Price, out priceDecimal);
        bool.TryParse(command.Active, out activeBool);


        // Update entity
        ingredient.Update(command.Description, priceDecimal, activeBool);

        // Save database
        _repository.Update(ingredient);

        return new CommandResult(true, new IngredientCommandResult(ingredient));
    }
}