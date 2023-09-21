using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Coffee.Domain.Commands;
using Coffee.Domain.Commands.Interfaces;
using Coffee.Domain.Handlers.ProductHandlers.PersonalizedCoffeeHandlers;

namespace Coffee.Api.Controllers.ProductsController.PersonalizedCoffeesController;

[ApiController]
[Route("")]
public class PersonalizedCoffeeControllerBase : ControllerBase
{
    private readonly PersonalizedCoffeeHandler _personalizedCoffeeHandler;

    public PersonalizedCoffeeControllerBase(PersonalizedCoffeeHandler personalizedCoffeeHandler)
    {
        _personalizedCoffeeHandler = personalizedCoffeeHandler;
    }

    protected async Task<IActionResult> ExecuteCommandAsync<T>(T command)
        where T : Command
    {
        try
        {
            command.SetUrlOfSite($"{Request.Scheme}://{Request.Host}");
            command.SetUser(User);

            // Encontra o método HandleAsync no PersonalizedCoffeeHandler para o tipo de comando correspondente
            MethodInfo? handleMethod = _personalizedCoffeeHandler.GetType()
                .GetMethods()
                .FirstOrDefault(m =>
                    m.Name == "HandleAsync" &&
                    m.GetParameters().FirstOrDefault()?.ParameterType == typeof(T)
                );

            if (handleMethod is null)
            {
                throw new Exception("Método HandleAsync não encontrado para o tipo de comando.");
            }

            var result = await (Task<ICommandResult>)handleMethod.Invoke(_personalizedCoffeeHandler, new object[] { command })!;

            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new CommandResult(false, "Erro ao realizar a requisição"));
        }
    }
}