using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.CoffeCommands;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.CoffeHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;


namespace Coffee.Api.Controllers.ProductsController.PersonalizedCoffeesController.CoffesController;

[ApiController]
[Route("")]
public partial class CoffeController : CoffeControllerBase
{
    private readonly CoffeHandler _coffeHandler;
    public CoffeController(CoffeHandler coffeHandler) : base(coffeHandler)
    {
        _coffeHandler = coffeHandler;
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpPost("v1/products/personalizedcoffees/coffees")]
    public async Task<IActionResult> Create([FromBody] CreateCoffeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/products/personalizedcoffees/coffees")]
    public async Task<IActionResult> GetAll(
        [FromServices] ICoffeRepository repository,
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

    [HttpGet("v1/products/personalizedcoffees/coffees/description")]
    public async Task<IActionResult> Get([FromBody] GetCoffeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpPut("v1/products/personalizedcoffees/coffees")]
    public async Task<IActionResult> Update([FromBody] UpdateCoffeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpDelete("v1/products/personalizedcoffees/coffees")]
    public async Task<IActionResult> Delete([FromBody] DeleteCoffeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}