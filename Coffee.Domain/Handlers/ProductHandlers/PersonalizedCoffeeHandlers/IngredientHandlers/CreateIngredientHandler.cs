using Coffee.Domain.Enums;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;

public class CreateIngredientHandler : Handler, IHandler<CreateIngredientCommand>
{

    private readonly IIngredientRepository _repository;

    public CreateIngredientHandler(IIngredientRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreateIngredientCommand command)
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
        if (ingredient is not null)
        {
            AddNotification(command.Description, "Ingrediente já cadastrado");
            return new CommandResult(false, Notifications);
        }

        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(command.Price, out priceDecimal);
        bool.TryParse(command.Active, out activeBool);


        // Build entity
        ingredient = new Ingredient(command.Description, priceDecimal, activeBool);

        // Save database
        await _repository.CreateAsync(ingredient);

        return new CommandResult(true, new IngredientCommandResult(ingredient));
    }
}