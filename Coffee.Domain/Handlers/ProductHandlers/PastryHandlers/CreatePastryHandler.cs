using Coffee.Domain.Enums;
using Coffee.Domain.Models.Product.Pastry;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PastryCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PastryHandlers;

public class CreatePastryHandler : Handler, IHandler<CreatePastryCommand>
{

    private readonly IPastryRepository _repository;

    public CreatePastryHandler(IPastryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePastryCommand command)
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

        var pastry = await _repository.GetByDescriptionAsync(command.Description);

        // Query pastry exist
        if (pastry is not null)
        {
            AddNotification(command.Description, "Acompanhamento já cadastrado");
            return new CommandResult(false, Notifications);
        }

        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(command.Price, out priceDecimal);
        bool.TryParse(command.Active, out activeBool);


        // Build entity
        pastry = new Pastry(command.Description, priceDecimal, activeBool);

        // Save database
        await _repository.CreateAsync(pastry);

        return new CommandResult(true, new PastryCommandResult(pastry));
    }
}