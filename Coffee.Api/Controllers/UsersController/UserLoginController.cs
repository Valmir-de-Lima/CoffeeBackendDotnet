using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.UserHandlers;
using Coffee.Domain.Commands;

namespace Coffee.Api.Controllers.UsersController;

public partial class UserController : ControllerBase
{
    [HttpPost("v1/users/login")]
    public async Task<IActionResult> Login(
    [FromBody] LoginUserCommand command,
    [FromServices] UserHandler handler
    )
    {
        try
        {
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao efetuar o login."
            ));
        }
    }

    [HttpPost("v1/users/login/refresh")]
    public async Task<IActionResult> Refresh(
    [FromBody] RefreshLoginUserCommand command,
    [FromServices] UserHandler handler
    )
    {
        try
        {
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao efetuar o login."
            ));
        }
    }

    [HttpPost("v1/users/login/register")]
    public async Task<IActionResult> Register(
    [FromBody] RegisterUserCommand command,
    [FromServices] UserHandler handler
    )
    {
        try
        {
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao efetuar o registro."
            ));
        }
    }

    [HttpGet("v1/users/login/active/{id}")]
    public async Task<IActionResult> Active(
        [FromRoute] string id,
        [FromServices] UserHandler handler
    )
    {
        try
        {
            var command = new ActiveUserCommand(id);
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

    [HttpPost("v1/users/login/recovery-password")]
    public async Task<IActionResult> RecoveryPassword(
        [FromBody] RecoveryPasswordUserCommand command,
        [FromServices] UserHandler handler
    )
    {
        try
        {
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao acessar o banco de dados"
            ));
        }
    }

    [HttpGet("v1/users/login/recovery-password/{id}")]
    public async Task<IActionResult> ConfirmRecoveryPassword(
        [FromRoute] string id,
        [FromServices] UserHandler handler
    )
    {
        try
        {
            var command = new ConfirmRecoveryPasswordUserCommand(id);
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
            return Ok((CommandResult)await handler.HandleAsync(command));
        }
        catch
        {
            return StatusCode(500, new CommandResult(false,
                "Erro ao acessar o banco de dados"
            ));
        }
    }

    [HttpPost("v1/users/login/update-recovery-password")]
    public async Task<IActionResult> ConfirmRecoveryPassword(
        [FromBody] UpdateRecoveryPasswordUserCommand command,
        [FromServices] UserHandler handler
    )
    {
        try
        {
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
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
