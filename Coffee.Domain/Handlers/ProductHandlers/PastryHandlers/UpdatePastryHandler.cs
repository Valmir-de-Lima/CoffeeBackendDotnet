using Coffee.Domain.Enums;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PastryCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PastryHandlers;

public class UpdatePastryHandler : Handler, IHandler<UpdatePastryCommand>
{

    private readonly IPastryRepository _repository;

    public UpdatePastryHandler(IPastryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult> HandleAsync(UpdatePastryCommand command)
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
        if (pastry is null)
        {
            AddNotification(command.Description, "Acompanhamento não cadastrado");
            return new CommandResult(false, Notifications);
        }

        decimal priceDecimal;
        bool activeBool;
        decimal.TryParse(command.Price, out priceDecimal);
        bool.TryParse(command.Active, out activeBool);


        // Update entity
        pastry.Update(command.Description, priceDecimal, activeBool);

        // Save database
        _repository.Update(pastry);

        return new CommandResult(true, new PastryCommandResult(pastry));
    }
}