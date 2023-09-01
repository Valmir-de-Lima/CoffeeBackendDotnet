namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class GetUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "batman-wayne-com", EType.Manager)]
    [DataRow("robin-wayne-com", "robin-wayne-com", EType.Deliveryman)]
    [DataRow("superman-justiceleague-com", "superman-justiceleague-com", EType.Customer)]
    public async Task ShouldReturnTrueSuccessWhenUserAccessYourSelfAndDatasAreValids(string link, string linkToken, EType type)
    {
        var command = new GetUserCommand(link);

        command.SetUserType(type);
        command.SetUserName(linkToken);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("catwoman-wayne-com", "batman-wayne-com", EType.Manager)]
    [DataRow("robin-wayne-com", "batman-wayne-com", EType.Manager)]
    [DataRow("superman-justiceleague-com", "batman-wayne-com", EType.Manager)]
    public async Task ShouldReturnTrueSuccessWhenLinkExistsAndManagerAccess(string link, string linkToken, EType type)
    {
        var command = new GetUserCommand(link);

        command.SetUserType(type);
        command.SetUserName(linkToken);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("catwoman-catwoman-com", "batman-wayne-com", EType.Manager)]
    [DataRow("robin-robin-com", "batman-wayne-com", EType.Manager)]
    [DataRow("superman-superman-com", "batman-wayne-com", EType.Manager)]
    public async Task ShouldReturnFalseSuccessWhenLinkDontExistsAndManagerAccess(string link, string linkToken, EType type)
    {
        var command = new GetUserCommand(link);

        command.SetUserType(type);
        command.SetUserName(linkToken);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com", "robin-wayne-com", EType.Deliveryman)]
    [DataRow("robin-wayne-com", "superman-justiceleague-com", EType.Customer)]
    [DataRow("superman-justiceleague-com", "robin-wayne-com", EType.Deliveryman)]
    public async Task ShouldReturnFalseSuccessWhenUserAccessOthersLinksAndUserDontManager(string link, string linkToken, EType type)
    {
        var command = new GetUserCommand(link);

        command.SetUserType(type);
        command.SetUserName(linkToken);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }
}

