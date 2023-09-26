using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Handlers.ProductHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.ProductsController;

[ApiController]
[Route("")]
public partial class ProductController : ProductControllerBase
{
    private readonly ProductHandler _productHandler;
    public ProductController(ProductHandler productHandler) : base(productHandler)
    {
        _productHandler = productHandler;
    }

    [HttpPost("v1/products")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/products")]
    public async Task<IActionResult> GetAll(
        [FromServices] IProductRepository repository,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 25
    )
    {
        try
        {
            return Ok(new CommandResult(true, await repository.GetAllAsync(skip, take)));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao acessar o banco de dados"
            ));
        }
    }

    [HttpGet("v1/products/id")]
    public async Task<IActionResult> Get([FromBody] GetProductCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpDelete("v1/products")]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}
