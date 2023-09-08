namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class UpdateTypeUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());

    private UpdateTypeUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", EType.Barista)]
    [DataRow("catwoman@wayne.com", EType.Manager)]
    [DataRow("robin@wayne.com", EType.Customer)]
    [DataRow("superman@justiceleague.com", EType.Deliveryman)]
    public async Task ShouldReturnTrueSucessWhenDataAreValids(string adress, EType type)
    {
        _command.Email = adress;
        _command.Type = (int)type;

        _command.SetUserType(EType.Manager);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@batman.com", EType.Barista)]
    [DataRow("catwoman@cat.com", EType.Manager)]
    [DataRow("robin@boy.com", EType.Manager)]
    [DataRow("superman@man.com", EType.Manager)]
    public async Task ShouldReturnFalseSucessWhenEmailDontExists(string adress, EType type)
    {
        _command.Email = adress;
        _command.Type = (int)type;

        _command.SetUserType(EType.Manager);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", -1)]
    [DataRow("catwoman@wayne.com", 4)]
    [DataRow("robin@wayne.com", 5)]
    [DataRow("superman@justiceleague.com", -2)]
    public async Task ShouldReturnFalseSucessWhenTypeDontExists(string adress, int type)
    {
        _command.Email = adress;
        _command.Type = type;

        _command.SetUserType(EType.Manager);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", EType.Barista)]
    [DataRow("catwoman@wayne.com", EType.Manager)]
    [DataRow("robin@wayne.com", EType.Customer)]
    [DataRow("superman@justiceleague.com", EType.Deliveryman)]
    public async Task ShouldReturnFalseSucessWhenUserIsntManager(string adress, EType type)
    {
        _command.Email = adress;
        _command.Type = (int)type;

        _command.SetUserType(EType.Customer);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }
}

