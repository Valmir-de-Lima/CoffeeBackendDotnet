using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.UserHandlers;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.UsersController;

public partial class UserController : UserControllerBase
{
    [HttpPost("v1/users/manager")]
    public async Task<IActionResult> CreatePrimaryManager(
    [FromServices] IConfiguration config,
    [FromServices] UserHandler handler
    )
    {
        try
        {
            var urlOfSite = $"{Request.Scheme}://{Request.Host}";
            return Ok((CommandResult)await handler.HandleAsync(Configuration.CreatePrimaryManager(config, urlOfSite)));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao realizar a requisição"
            ));
        }
    }

    [Authorize(Roles = Configuration.MANAGER)]
    [HttpPut("v1/users/manager/update-type-user")]
    public async Task<IActionResult> UpdateTypeUser([FromBody] UpdateTypeUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}