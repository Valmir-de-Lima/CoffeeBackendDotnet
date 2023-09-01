namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class GetUserByEmailHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "batman-wayne-com", EType.Manager)]
    [DataRow("robin@wayne.com", "robin-wayne-com", EType.Deliveryman)]
    [DataRow("superman@justiceleague.com", "superman-justiceleague-com", EType.Customer)]
    public async Task ShouldReturnTrueSuccessWhenUserAccessYourSelfAndDatasAreValids(string addres, string link, EType type)
    {
        var command = new GetUserByEmailCommand();
        command.Email = addres;

        command.SetUserType(type);
        command.SetUserName(link);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("catwoman@wayne.com", "batman-wayne-com", EType.Manager)]
    [DataRow("robin@wayne.com", "batman-wayne-com", EType.Manager)]
    [DataRow("superman@justiceleague.com", "batman-wayne-com", EType.Manager)]
    public async Task ShouldReturnTrueSuccessWhenEmailExistsAndManagerAccess(string addres, string link, EType type)
    {
        var command = new GetUserByEmailCommand();
        command.Email = addres;

        command.SetUserType(type);
        command.SetUserName(link);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@batman.com", "batman-wayne-com", EType.Manager)]
    [DataRow("robin@robin.com", "batman-wayne-com", EType.Manager)]
    [DataRow("superman@superman.com", "batman-wayne-com", EType.Manager)]
    public async Task ShouldReturnFalseSuccessWhenEmailDontExistsAndManagerAccess(string addres, string link, EType type)
    {
        var command = new GetUserByEmailCommand();
        command.Email = addres;

        command.SetUserType(type);
        command.SetUserName(link);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "robin-wayne-com", EType.Deliveryman)]
    [DataRow("robin@wayne.com", "superman-justiceleague-com", EType.Customer)]
    [DataRow("superman@justiceleague.com", "robin-wayne-com", EType.Deliveryman)]
    public async Task ShouldReturnFalseSuccessWhenUserAccessOthersEmailsAndUserDontManager(string addres, string link, EType type)
    {
        var command = new GetUserByEmailCommand();
        command.Email = addres;

        command.SetUserType(type);
        command.SetUserName(link);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }
}

