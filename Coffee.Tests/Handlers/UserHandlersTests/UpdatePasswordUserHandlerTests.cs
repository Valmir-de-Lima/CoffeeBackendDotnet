namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class UpdatePasswordUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private readonly UpdatePasswordUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "Teste.31122022", "1234.Teste")]
    [DataRow("catwoman-wayne-com", "Teste.31122022", "1234.Teste")]
    [DataRow("robin-wayne-com", "Teste.31122022", "1234.Teste")]
    [DataRow("superman-justiceleague-com", "Teste.31122022", "1234.Teste")]
    public async Task ShouldReturnTrueSuccessWhenDatasAreValids(string userName, string oldPassword, string newPassword)
    {
        _command.SetUserName(userName);
        _command.OldPassword = oldPassword;
        _command.NewPassword = newPassword;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "", "1234.Teste")]
    [DataRow("catwoman-wayne-com", "Teste31122022", "1234.Teste")]
    [DataRow("robin-wayne-com", "Teste.31122023", "1234.Teste")]
    [DataRow("superman-justiceleague-com", "Teste.teste", "1234.Teste")]
    public async Task ShouldReturnFalseSuccessWhenOldPasswordIsInvalid(string userName, string oldPassword, string newPassword)
    {
        _command.SetUserName(userName);
        _command.OldPassword = oldPassword;
        _command.NewPassword = newPassword;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "", "1234.Te")]
    [DataRow("catwoman-wayne-com", "Teste31122022", "1234Teste")]
    [DataRow("robin-wayne-com", "Teste.31122023", "teste.Teste")]
    [DataRow("superman-justiceleague-com", "Teste.teste", "")]
    public async Task ShouldReturnFalseSuccessWhenNewPasswordIsInvalid(string userName, string oldPassword, string newPassword)
    {
        _command.SetUserName(userName);
        _command.OldPassword = oldPassword;
        _command.NewPassword = newPassword;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }
}

