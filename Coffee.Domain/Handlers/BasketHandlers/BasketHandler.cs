using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.BasketHandlers;

public class BasketHandler : Handler,
    IHandler<CreateBasketCommand>,
    IHandler<GetBasketCommand>,
    IHandler<DeleteBasketCommand>,
    IHandler<AddProductBasketCommand>,
    IHandler<RemoveProductBasketCommand>,
    IHandler<IncreaseQuantityProductBasketCommand>,
    IHandler<DecreaseQuantityProductBasketCommand>
{
    private readonly IBasketRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPersonalizedCoffeeRepository _personalizedCoffeeRepository;
    private readonly IPastryRepository _pastryRepository;


    public BasketHandler(
        IBasketRepository repository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IPersonalizedCoffeeRepository personalizedCoffeeRepository,
        IPastryRepository pastryRepository
        )
    {
        _repository = repository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _personalizedCoffeeRepository = personalizedCoffeeRepository;
        _pastryRepository = pastryRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreateBasketCommand command)
    {
        return await new CreateBasketHandler(_repository, _userRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetBasketCommand command)
    {
        return await new GetBasketHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DeleteBasketCommand command)
    {
        return await new DeleteBasketHandler(_repository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(AddProductBasketCommand command)
    {
        return await new AddProductBasketHandler(
            _repository,
            _productRepository,
            _personalizedCoffeeRepository,
            _pastryRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(RemoveProductBasketCommand command)
    {
        return await new RemoveProductBasketHandler(_repository, _productRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(IncreaseQuantityProductBasketCommand command)
    {
        return await new IncreaseQuantityProductBasketHandler(_repository, _productRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(DecreaseQuantityProductBasketCommand command)
    {
        return await new DecreaseQuantityProductBasketHandler(_repository, _productRepository).HandleAsync(command);
    }
}