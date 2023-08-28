using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.UserCommands;

namespace Coffee.Api.Controllers.UsersController;

public partial class UserController : UserControllerBase
{
    [HttpPost("v1/users/login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPost("v1/users/login/refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshLoginUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpPost("v1/users/login/register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/users/login/active/{id}")]
    public async Task<IActionResult> Active([FromRoute] string id)
    {
        var command = new ActiveUserCommand(id);
        return await ExecuteCommandAsync(command);
    }

    [HttpPost("v1/users/login/recovery-password")]
    public async Task<IActionResult> RecoveryPassword([FromBody] RecoveryPasswordUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [HttpGet("v1/users/login/recovery-password/{id}")]
    public async Task<IActionResult> ConfirmRecoveryPassword([FromRoute] string id)
    {
        var command = new ConfirmRecoveryPasswordUserCommand(id);
        return await ExecuteCommandAsync(command);
    }

    [HttpPost("v1/users/login/update-recovery-password")]
    public async Task<IActionResult> ConfirmRecoveryPassword([FromBody] UpdateRecoveryPasswordUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}
