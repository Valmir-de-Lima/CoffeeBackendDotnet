using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.ProductsController.PersonalizedCoffeesController;

[ApiController]
[Route("")]
public partial class PersonalizedCoffeeController : PersonalizedCoffeeControllerBase
{
    private readonly PersonalizedCoffeeHandler _personalizedCoffeeHandler;
    public PersonalizedCoffeeController(PersonalizedCoffeeHandler personalizedCoffeeHandler) : base(personalizedCoffeeHandler)
    {
        _personalizedCoffeeHandler = personalizedCoffeeHandler;
    }

    [HttpPost("v1/products/personalizedcoffees")]
    public async Task<IActionResult> Create([FromBody] CreatePersonalizedCoffeeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/products/personalizedcoffees")]
    public async Task<IActionResult> GetAll(
        [FromServices] IPersonalizedCoffeeRepository repository,
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

    [HttpGet("v1/products/personalizedcoffees/id")]
    public async Task<IActionResult> Get([FromBody] GetPersonalizedCoffeeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/products/personalizedcoffees")]
    public async Task<IActionResult> Update([FromBody] UpdatePersonalizedCoffeeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpDelete("v1/products/personalizedcoffees")]
    public async Task<IActionResult> Delete([FromBody] DeletePersonalizedCoffeeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/products/personalizedcoffees/add-ingredient")]
    public async Task<IActionResult> Update([FromBody] AddIngredientPersonalizedCoffeeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPut("v1/products/personalizedcoffees/remove-ingredient")]
    public async Task<IActionResult> Update([FromBody] RemoveIngredientPersonalizedCoffeeCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}