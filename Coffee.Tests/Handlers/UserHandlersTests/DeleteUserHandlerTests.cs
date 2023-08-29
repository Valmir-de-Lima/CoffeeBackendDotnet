namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class DeleteUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "Teste.31122022")]
    [DataRow("robin@wayne.com", "Teste.31122022")]
    [DataRow("superman@justiceleague.com", "Teste.31122022")]
    public async Task ShouldReturnTrueSuccessWhenEmailExists(string addres, string password)
    {
        var command = new DeleteUserCommand();
        command.Email = addres;
        command.Password = password;

        command.SetUserType(EType.Manager);
        command.SetUserName(addres.Replace("@", "-").Replace(".", "-"));
        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@batman.com", "Teste.31122022")]
    [DataRow("robin@robin.com", "Teste.31122022")]
    [DataRow("superman@justice.com", "Teste.31122022")]
    public async Task ShouldReturnFalseSucessWhenEmailDontExists(string addres, string password)
    {
        var command = new DeleteUserCommand();
        command.Email = addres;
        command.Password = password;

        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }

}

