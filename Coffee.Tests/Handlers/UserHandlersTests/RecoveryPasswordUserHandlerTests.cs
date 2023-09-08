namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class RecoveryPasswordUserHandlerTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com")]
    [DataRow("catwoman@wayne.com")]
    [DataRow("robin@wayne.com")]
    [DataRow("superman@justiceleague.com")]
    public async Task ShouldReturnTrueSuccessWhenEmailExists(string addres)
    {
        var command = new RecoveryPasswordUserCommand();
        command.Email = addres;

        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman.com")]
    [DataRow("catwoman@.com")]
    [DataRow("robin@robin")]
    [DataRow("")]
    public async Task ShouldReturnFalseSuccessWhenEmailIsInvalid(string addres)
    {
        var command = new RecoveryPasswordUserCommand();
        command.Email = addres;

        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }


    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@batman.com")]
    [DataRow("catwoman@catwoman.com")]
    [DataRow("robin@robin.com")]
    [DataRow("superman@superman.com")]
    public async Task ShouldReturnFalseSuccessWhenEmailDontExists(string addres)
    {
        var command = new RecoveryPasswordUserCommand();
        command.Email = addres;

        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }
}

