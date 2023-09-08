namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class UpdateUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private readonly UpdateUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "Bruce Wayne")]
    [DataRow("catwoman-wayne-com", "Jessica Wayne")]
    [DataRow("robin-wayne-com", "The prodigy boy")]
    [DataRow("superman-justiceleague-com", "Clark Kent")]
    public async Task ShouldReturnTrueSuccessWhenDatasAreValids(string userName, string newName)
    {
        _command.SetUserName(userName);
        _command.Name = newName;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "")]
    [DataRow("catwoman-wayne-com", "Js")]
    [DataRow("robin-wayne-com", "oy")]
    [DataRow("superman-justiceleague-com", "Ca")]
    public async Task ShouldReturnFalseSucessWhenNameIsInvalid(string userName, string newName)
    {
        _command.SetUserName(userName);
        _command.Name = newName;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-com", "Bruce Wayne")]
    [DataRow("catwoman-wayne", "Jessica Wayne")]
    [DataRow("robin-wayne-co", "The prodigy boy")]
    [DataRow("", "Clark Kent")]
    public async Task ShouldReturnFalseSucessWhenLinkDontExists(string userName, string newName)
    {
        _command.SetUserName(userName);
        _command.Name = newName;

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }
}

