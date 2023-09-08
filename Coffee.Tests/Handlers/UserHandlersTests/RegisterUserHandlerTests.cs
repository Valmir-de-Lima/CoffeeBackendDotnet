namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class RegisterUserHandlersTests
{
    private readonly UserHandler _handler = new UserHandler(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private readonly RegisterUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@justice.com", "Teste.31122022")]
    [DataRow("robin", "robin@justice.com", "Teste.31122022")]
    [DataRow("superman", "superman@justice.com", "Teste.31122022")]
    public async Task ShouldReturnTrueSuccessWhenDatasAreValids(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("ba", "batman@justice.com", "Teste.31122022")]
    [DataRow("", "robin@justice.com", "Teste.31122022")]
    [DataRow("superman superman superman superman superman superman", "superman@justice.com", "Teste.31122022")]
    public async Task ShouldReturnFalseSucessWhenNameIsInvalid(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@wayne.com", "Teste.31122022")]
    [DataRow("robin", "robin@wayne.com", "Teste.31122022")]
    [DataRow("superman", "superman@justiceleague.com", "Teste.31122022")]
    public async Task ShouldReturnFalseSucessWhenEmailExists(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@justice.com", "Teste31122022")]
    [DataRow("robin", "robin@justice.com", "Teste.abc")]
    [DataRow("superman", "superman@justice.com", "Tes.311")]
    public async Task ShouldReturnFalseSuccessWhenPasswordIsInvalid(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

}

