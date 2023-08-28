using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.UserHandlers;
using Coffee.Domain.Repositories.Interfaces;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.UsersController;

public partial class UserController : UserControllerBase
{
    [Authorize(Roles = Configuration.MANAGER)]
    [HttpGet("v1/users")]
    public async Task<IActionResult> GetAll(
            [FromServices] IUserRepository repository,
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

    [Authorize]
    [HttpGet("v1/users/{link}")]
    public async Task<IActionResult> GetByLink(
            [FromRoute] string link,
            [FromServices] UserHandler handler
    )
    {
        try
        {
            var command = new GetUserCommand(link);
            command.SetUser(User);
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao acessar o banco de dados"
            ));
        }
    }

    [Authorize]
    [HttpGet("v1/users/email")]
    public async Task<IActionResult> GetByEmail(
        [FromBody] GetUserByEmailCommand command,
        [FromServices] UserHandler handler
)
    {
        try
        {
            command.SetUser(User);
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao acessar o banco de dados"
            ));
        }
    }

}

