namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class GetUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());

    [TestMethod]
    [DataTestMethod]
    //[DataRow("batman-wayne-com")]
    [DataRow("robin-wayne-com")]
    //[DataRow("superman-justiceleague-com")]
    public async Task ShouldReturnTrueSuccessWhenLinkExists(string link)
    {
        var command = new GetUserCommand(link);
        command.SetUserName(link);
        command.SetUserType(EType.Customer);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-batman-com")]
    [DataRow("robin-robin-com")]
    [DataRow("superman-justice-com")]
    public async Task ShouldReturnFalseSucessWhenEmailDontExists(string link)
    {
        var command = new GetUserCommand(link);
        command.SetUserName(link);
        command.SetUserType(EType.Customer);
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }

}

