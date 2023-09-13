using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.ProductCommands.PastryCommands;
using Coffee.Domain.Handlers.ProductHandlers.PastryHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.ProductsController.PastrysController;

[ApiController]
[Route("")]
public partial class PastryController : PastryControllerBase
{
    private readonly PastryHandler _pastryHandler;
    public PastryController(PastryHandler pastryHandler) : base(pastryHandler)
    {
        _pastryHandler = pastryHandler;
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpPost("v1/products/pastrys")]
    public async Task<IActionResult> Create([FromBody] CreatePastryCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/products/pastrys")]
    public async Task<IActionResult> GetAll(
        [FromServices] IPastryRepository repository,
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

    [HttpGet("v1/products/pastrys/description")]
    public async Task<IActionResult> Get([FromBody] GetPastryCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpPut("v1/products/pastrys")]
    public async Task<IActionResult> Update([FromBody] UpdatePastryCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = $"{Configuration.MANAGER},{Configuration.BARISTA}")]
    [HttpDelete("v1/products/pastrys")]
    public async Task<IActionResult> Delete([FromBody] DeletePastryCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}