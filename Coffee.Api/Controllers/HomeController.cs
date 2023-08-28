using Microsoft.AspNetCore.Mvc;

namespace Coffee.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get(
        [FromServices] IConfiguration config)
    {
        var version = config.GetValue<string>("Version");
        return Ok(new
        {
            version
        });
    }
}
