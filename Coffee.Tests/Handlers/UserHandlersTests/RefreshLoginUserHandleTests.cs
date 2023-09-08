using System.Text.RegularExpressions;
namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class RefreshLoginUserHandlerTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private RefreshLoginUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "Teste.31122022", EType.Manager)]
    [DataRow("catwoman@wayne.com", "Teste.31122022", EType.Barista)]
    [DataRow("robin@wayne.com", "Teste.31122022", EType.Deliveryman)]
    [DataRow("superman@justiceleague.com", "Teste.31122022", EType.Customer)]
    public async Task ShouldReturnTrueSucessWhenDataAreValids(string adress, string password, EType type)
    {
        _command = await LoginUser(adress, password, type);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    private async Task<RefreshLoginUserCommand> LoginUser(string adress, string password, EType type)
    {
        LoginUserCommand command = new();
        command.Email = adress;
        command.Password = password;
        command.SetUserName(command.Email.Replace("@", "-").Replace(".", "-"));
        command.SetUserType(type);

        var _result = (CommandResult)await _handler.HandleAsync(command);

        var data = _result.Data?.ToString();
        if (data is null)
            data = "";

        return ResultDataToRefreshLoginUserCommand(data);
    }

    private RefreshLoginUserCommand ResultDataToRefreshLoginUserCommand(string resultData = "")
    {
        RefreshLoginUserCommand refreshLoginUserCommand = new();

        string inputString = resultData;

        // Remova os caracteres "{" e "}" da string
        inputString = inputString.Trim('{', '}');

        // Divida a string em pares chave-valor usando vírgulas como delimitador
        string[] keyValuePairs = inputString.Split(',');

        // Crie um dicionário para armazenar os valores
        var data = new Dictionary<string, string>();

        foreach (string keyValuePair in keyValuePairs)
        {
            // Use uma expressão regular para encontrar a chave e o valor
            Match match = Regex.Match(keyValuePair.Trim(), @"(\w+)\s*=\s*([^,]+)");

            // Verifique se a correspondência foi encontrada
            if (match.Success)
            {
                string key = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();

                data[key] = value;
            }
        }

        // Acesse os valores de "token" e "refreshToken" diretamente no dicionário
        if (data.TryGetValue("token", out string? token))
        {
            refreshLoginUserCommand.Token = token;
        }

        if (data.TryGetValue("refreshToken", out string? refreshToken))
        {
            refreshLoginUserCommand.RefreshToken = refreshToken;
        }
        return refreshLoginUserCommand;
    }

}

