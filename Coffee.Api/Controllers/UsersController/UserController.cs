using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coffee.Domain.Commands.UserCommands;
using Coffee.Domain.Handlers.UserHandlers;

namespace Coffee.Api.Controllers.UsersController;

[ApiController]
[Route("")]
public partial class UserController : UserControllerBase
{
    private readonly UserHandler _userHandler;
    public UserController(UserHandler userHandler) : base(userHandler)
    {
        _userHandler = userHandler;
    }

    [Authorize(Roles = Configuration.MANAGER)]
    [HttpPost("v1/users")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize]
    [HttpPut("v1/users")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize(Roles = Configuration.MANAGER)]
    [HttpDelete("v1/users")]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }

    [Authorize]
    [HttpPut("v1/users/password")]
    public async Task<IActionResult> UpdatePasswordUser([FromBody] UpdatePasswordUserCommand command)
    {
        return await ExecuteCommandAsync(command);
    }
}