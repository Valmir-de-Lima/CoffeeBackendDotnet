using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.BasketCommands;
using Coffee.Domain.Handlers.BasketHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.BasketsController;

[ApiController]
[Route("")]
public partial class BasketController : BasketControllerBase
{
    private readonly BasketHandler _basketHandler;
    public BasketController(BasketHandler basketHandler) : base(basketHandler)
    {
        _basketHandler = basketHandler;
    }

    [HttpPost("v1/baskets")]
    public async Task<IActionResult> Create([FromBody] CreateBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/baskets")]
    public async Task<IActionResult> GetAll(
        [FromServices] IBasketRepository repository,
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

    [HttpGet("v1/baskets/id")]
    public async Task<IActionResult> Get([FromBody] GetBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpDelete("v1/baskets")]
    public async Task<IActionResult> Delete([FromBody] DeleteBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/baskets/add-product")]
    public async Task<IActionResult> Update([FromBody] AddProductBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/baskets/remove-product")]
    public async Task<IActionResult> Update([FromBody] RemoveProductBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/baskets/increase-quantity-product")]
    public async Task<IActionResult> Update([FromBody] IncreaseQuantityProductBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/baskets/decrease-quantity-product")]
    public async Task<IActionResult> Update([FromBody] DecreaseQuantityProductBasketCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}