using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

public class PersonalizedCoffeeHandler : Handler,
    IHandler<CreatePersonalizedCoffeeCommand>,
    IHandler<GetPersonalizedCoffeeCommand>,
    IHandler<UpdatePersonalizedCoffeeCommand>,
    IHandler<DeletePersonalizedCoffeeCommand>,
    IHandler<AddIngredientPersonalizedCoffeeCommand>,
    IHandler<RemoveIngredientPersonalizedCoffeeCommand>
{
    private readonly IPersonalizedCoffeeRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICoffeRepository _coffeRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public PersonalizedCoffeeHandler(IPersonalizedCoffeeRepository repository, IUserRepository userRepository, ICoffeRepository coffeRepository, IIngredientRepository ingredientRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _coffeRepository = coffeRepository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreatePersonalizedCoffeeCommand command)
    {
        return await new CreatePersonalizedCoffeeHandler(_repository, _userRepository, _coffeRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetPersonalizedCoffeeCommand command)
    {
        return await new GetPersonalizedCoffeeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(UpdatePersonalizedCoffeeCommand command)
    {
        return await new UpdatePersonalizedCoffeeHandler(_repository, _coffeRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeletePersonalizedCoffeeCommand command)
    {
        return await new DeletePersonalizedCoffeeHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(AddIngredientPersonalizedCoffeeCommand command)
    {
        return await new AddIngredientPersonalizedCoffeeHandler(_repository, _ingredientRepository).HandleAsync(command);
    }
    public async Task<ICommandResult> HandleAsync(RemoveIngredientPersonalizedCoffeeCommand command)
    {
        return await new RemoveIngredientPersonalizedCoffeeHandler(_repository, _ingredientRepository).HandleAsync(command);
    }
}