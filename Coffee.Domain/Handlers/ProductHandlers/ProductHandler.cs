using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Handlers.Interfaces;
using Coffee.Domain.Repositories.Interfaces;

namespace Coffee.Domain.Handlers.ProductHandlers;

public class ProductHandler : Handler,
    IHandler<CreateProductCommand>,
    IHandler<GetProductCommand>,
    IHandler<DeleteProductCommand>
//    IHandler<AddProductCommand>,
//    IHandler<RemoveProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IBasketRepository _basketRepository;
    private readonly IPersonalizedCoffeeRepository _personalizedCoffeeRepository;
    private readonly IPastryRepository _pastryRepository;
    private readonly IUserRepository _userRepository;

    public ProductHandler(
        IProductRepository repository,
        IBasketRepository basketRepository,
        IPersonalizedCoffeeRepository personalizedCoffeeRepository,
        IPastryRepository pastryRepository,
        IUserRepository userRepository
        )
    {
        _repository = repository;
        _basketRepository = basketRepository;
        _personalizedCoffeeRepository = personalizedCoffeeRepository;
        _pastryRepository = pastryRepository;
        _userRepository = userRepository;
    }

    public async Task<ICommandResult> HandleAsync(CreateProductCommand command)
    {
        return await new CreateProductHandler(
            _repository,
            _basketRepository,
            _personalizedCoffeeRepository,
            _pastryRepository,
            _userRepository).HandleAsync(command);
    }

    public async Task<ICommandResult> HandleAsync(GetProductCommand command)
    {
        return await new GetProductHandler(_repository).HandleAsync(command);
    }

    //    public async Task<ICommandResult> HandleAsync(UpdateProductCommand command)
    //    {
    //        return await new UpdateProductHandler(_repository).HandleAsync(command);
    //    }

    public async Task<ICommandResult> HandleAsync(DeleteProductCommand command)
    {
        return await new DeleteProductHandler(_repository).HandleAsync(command);
    }

    //    public async Task<ICommandResult> HandleAsync(AddProductCommand command)
    //    {
    //        return await new AddProductHandler(_repository).HandleAsync(command);
    //    }
    //    public async Task<ICommandResult> HandleAsync(RemoveProductCommand command)
    //    {
    //        return await new RemoveProductHandler(_repository).HandleAsync(command);
    //    }
}