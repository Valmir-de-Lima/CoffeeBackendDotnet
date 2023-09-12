using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers.IngredientHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;


namespace Coffee.Api.Controllers.ProductsController.PersonalizedCoffeesController.IngredientsController;

[ApiController]
[Route("")]
public partial class IngredientController : IngredientControllerBase
{
    private readonly IngredientHandler _ingredientHandler;
    public IngredientController(IngredientHandler ingredientHandler) : base(ingredientHandler)
    {
        _ingredientHandler = ingredientHandler;
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpPost("v1/products/personalizedcoffees/ingredients")]
    public async Task<IActionResult> Create([FromBody] CreateIngredientCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/products/personalizedcoffees/ingredients")]
    public async Task<IActionResult> GetAll(
        [FromServices] IIngredientRepository repository,
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

    [HttpGet("v1/products/personalizedcoffees/ingredients/description")]
    public async Task<IActionResult> GetIngredient([FromBody] GetIngredientCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpPut("v1/products/personalizedcoffees/ingredients")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateIngredientCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpDelete("v1/products/personalizedcoffees/ingredients")]
    public async Task<IActionResult> Delete([FromBody] DeleteIngredientCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}