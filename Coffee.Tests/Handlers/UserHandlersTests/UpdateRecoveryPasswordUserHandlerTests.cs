using System.Text.RegularExpressions;
namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class UpdateRecoveryPasswordUserHandlerTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private UpdateRecoveryPasswordUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "1234.Teste")]
    [DataRow("catwoman@wayne.com", "1234.Teste")]
    [DataRow("robin@wayne.com", "1234.Teste")]
    [DataRow("superman@justiceleague.com", "1234.Teste")]
    public async Task ShouldReturnTrueSucessWhenDataAreValids(string adress, string password)
    {
        _command = await RecoveryUser(adress);
        _command.Password = password;

        ConfirmRecoveryPasswordUserCommand confirm = new(_command.Id);
        await _handler.HandleAsync(confirm);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "")]
    [DataRow("catwoman@wayne.com", "123.Tes")]
    [DataRow("robin@wayne.com", "1234Teste")]
    [DataRow("superman@justiceleague.com", "1234.321")]
    public async Task ShouldReturnFalseSucessWhenPasswordIsInvalid(string adress, string password)
    {
        _command = await RecoveryUser(adress);
        _command.Password = password;

        ConfirmRecoveryPasswordUserCommand confirm = new(_command.Id);
        await _handler.HandleAsync(confirm);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }


    private async Task<UpdateRecoveryPasswordUserCommand> RecoveryUser(string adress)
    {
        RecoveryPasswordUserCommand command = new();
        command.Email = adress;

        await _handler.HandleAsync(command);

        return ResultDataToUpdateRecoveryPasswordUserCommand(Configuration.EmailBody);
    }

    private UpdateRecoveryPasswordUserCommand ResultDataToUpdateRecoveryPasswordUserCommand(string resultData = "")
    {
        UpdateRecoveryPasswordUserCommand updateRecoveryPasswordUserCommand = new();

        string inputString = resultData;

        // Use uma expressão regular para encontrar o ID entre as tags de âncora <a> e o código de autenticação.
        string pattern = @"href=""/v1/users/login/recovery-password/(\S+?)"".*?código de autenticação quando for solicitado: (\S+?)</p>";

        // Execute a expressão regular na string de entrada.
        Match match = Regex.Match(inputString, pattern);

        // Verifique se encontramos uma correspondência.
        if (match.Success)
        {
            // O ID estará no grupo 1 e o código de autenticação estará no grupo 2.
            string id = match.Groups[1].Value;
            string codigoAutenticacao = match.Groups[2].Value;

            updateRecoveryPasswordUserCommand.Id = id;
            updateRecoveryPasswordUserCommand.RecoveryPassword = codigoAutenticacao;
        }
        return updateRecoveryPasswordUserCommand;
    }

}

