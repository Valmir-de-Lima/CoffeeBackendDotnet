namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class LoginUserHandlersTests
{
    private readonly UserHandler _handler = new(new MockUserRepository(), new MockTokenService(), new MockEmailService());
    private readonly LoginUserCommand _command = new();


    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "Teste.31122022", EType.Manager)]
    [DataRow("catwoman@wayne.com", "Teste.31122022", EType.Barista)]
    [DataRow("robin@wayne.com", "Teste.31122022", EType.Deliveryman)]
    [DataRow("superman@justiceleague.com", "Teste.31122022", EType.Customer)]
    public async Task ShouldReturnTrueSuccessWhenDatasAreValids(string addres, string password, EType type)
    {
        _command.Email = addres;
        _command.Password = password;
        _command.SetUserName(addres.Replace("@", "-").Replace(".", "-"));
        _command.SetUserType(type);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsTrue(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@batman.com", "Teste.31122022", EType.Manager)]
    [DataRow("catwoman@catwoman.com", "Teste.31122022", EType.Barista)]
    [DataRow("robin@robin.com", "Teste.31122022", EType.Deliveryman)]
    [DataRow("superman@justice.com", "Teste.31122022", EType.Customer)]
    public async Task ShouldReturnFalseSucessWhenEmailDontExists(string addres, string password, EType type)
    {
        _command.Email = addres;
        _command.Password = password;
        _command.SetUserName(addres.Replace("@", "-").Replace(".", "-"));
        _command.SetUserType(type);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "", EType.Manager)]
    [DataRow("catwoman@wayne.com", "Teste.3112", EType.Barista)]
    [DataRow("robin@wayne.com", "123455", EType.Deliveryman)]
    [DataRow("superman@justiceleague.com", "123456123456", EType.Customer)]
    public async Task ShouldReturnFalseSucessWhenPasswordDontMatch(string addres, string password, EType type)
    {
        _command.Email = addres;
        _command.Password = password;
        _command.SetUserName(addres.Replace("@", "-").Replace(".", "-"));
        _command.SetUserType(type);

        var _result = (CommandResult)await _handler.HandleAsync(_command);

        Assert.IsFalse(_result.Success);
    }
}

