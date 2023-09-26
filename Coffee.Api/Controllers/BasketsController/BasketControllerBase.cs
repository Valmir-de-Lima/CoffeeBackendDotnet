using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Handlers.BasketHandlers;

namespace Coffee.Api.Controllers.BasketsController;

[ApiController]
[Route("")]
public class BasketControllerBase : ControllerBase
{
    private readonly BasketHandler _basketHandler;

    public BasketControllerBase(BasketHandler basketHandler)
    {
        _basketHandler = basketHandler;
    }

    protected async Task<IActionResult> ExecuteCommandAsync<T>(T command)
        where T : Command
    {
        try
        {
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
            command.SetUser(User);

            // Encontra o método HandleAsync no PersonalizedCoffeeHandler para o tipo de comando correspondente
            MethodInfo? handleMethod = _basketHandler.GetType()
                .GetMethods()
                .FirstOrDefault(m =>
                    m.Name == "HandleAsync" &&
                    m.GetParameters().FirstOrDefault()?.ParameterType == typeof(T)
                );

            if (handleMethod is null)
            {
                throw new Exception("Método HandleAsync não encontrado para o tipo de comando.");
            }

            var result = await (Task<ICommandResult>)handleMethod.Invoke(_basketHandler, new object[] { command })!;

            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new CommandResult(false, "Erro ao realizar a requisição"));
        }
    }
}