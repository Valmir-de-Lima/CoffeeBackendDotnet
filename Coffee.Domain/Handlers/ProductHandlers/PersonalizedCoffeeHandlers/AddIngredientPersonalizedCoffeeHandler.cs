using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;


namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

public class AddIngredientPersonalizedCoffeeHandler : Handler, IHandler<AddIngredientPersonalizedCoffeeCommand>
{

    private readonly IPersonalizedCoffeeRepository _repository;
    private readonly IIngredientRepository _ingredientRepository;

    public AddIngredientPersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository, IIngredientRepository ingredientRepository)
    {
        _repository = repository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<ICommandResult> HandleAsync(AddIngredientPersonalizedCoffeeCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, Notifications);
        }

        var personalizedCoffee = await _repository.GetByIdWithIngredientAsync(new Guid(command.PersonalizedCoffeeId));

        // Query personalized coffee exist
        if (personalizedCoffee is null)
        {
            AddNotification(command.PersonalizedCoffeeId, "Café personalizado não cadastrado");
            return new CommandResult(false, Notifications);
        }

        var ingredient = await _ingredientRepository.GetByIdAsync(new Guid(command.IngredientId));

        // Query ingredient exist
        if (ingredient is null)
        {
            AddNotification(command.IngredientId, "Ingrediente não cadastrado");
            return new CommandResult(false, Notifications);
        }

        // Query ingredient available
        if (!ingredient.Active)
        {
            AddNotification(command.IngredientId, "Ingrediente não disponível");
            return new CommandResult(false, Notifications);
        }

        // Query ingredient selected
        if (personalizedCoffee.SelectedIngredient(ingredient))
        {
            AddNotification(command.IngredientId, "Ingrediente já selecionado");
            return new CommandResult(false, Notifications);
        }

        // update model
        personalizedCoffee.AddIngredient(ingredient);

        // Save database
        _repository.Update(personalizedCoffee);

        return new CommandResult(true, new PersonalizedCoffeeCommandResult(personalizedCoffee));
    }
}