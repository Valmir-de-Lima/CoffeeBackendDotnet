namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class LoginUserCommandTests
{
    private readonly LoginUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", "Teste.31122022")]
    [DataRow("robin@wayne.com", "Teste.31122022")]
    [DataRow("superman@justiceleague.com", "Teste.31122022")]
    public void ShouldReturnValidCommandWhenDatasAreValids(string addres, string password)
    {
        _command.Email = addres;
        _command.Password = password;

        _command.Validate();
        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("@wayne.com", "Teste.31122022")]
    [DataRow("robin@.com", "Teste.31122022")]
    [DataRow("supermanjusticeleague.com", "Teste.31122022")]
    public void ShouldReturnInvalidCommandWhenEmailIsInvalid(string addres, string password)
    {
        _command.Email = addres;
        _command.Password = password;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }
}

