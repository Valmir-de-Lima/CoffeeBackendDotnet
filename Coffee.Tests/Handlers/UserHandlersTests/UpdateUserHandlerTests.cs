namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class UpdateUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private readonly UpdateUserCommand _command = new();

    // [TestMethod]
    // [DataTestMethod]
    // [DataRow("batman")]
    // [DataRow("robin")]
    // [DataRow("superman")]
    // public async Task ShouldReturnTrueSuccessWhenDatasAreValids(string name)
    // {
    //     _command.Name = name;

    //     var _result = (CommandResult)await _handler.HandleAsync(_command);

    //     Assert.IsFalse(_result.Success); // Para corrigir
    // }

    [TestMethod]
    [DataTestMethod]
    [DataRow("ba")]
    [DataRow("")]
    [DataRow("superman superman superman superman superman superman")]
    public async Task ShouldReturnFalseSucessWhenNameIsInvalid(string name)
    {
        _command.Name = name;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }
}

