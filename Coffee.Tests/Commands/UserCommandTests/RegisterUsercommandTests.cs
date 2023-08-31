namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class RegisterUserCommandTests
{
    private RegisterUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@wayne.com", "Teste.31122022")]
    [DataRow("robin", "robin@wayne.com", "Teste.31122022")]
    [DataRow("superman", "superman@justiceleague.com", "Teste.31122022")]
    public void ShouldReturnValidCommandWhenDatasAreValids(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        _command.Validate();
        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("ba", "batman@wayne.com", "Teste.31122022")]
    [DataRow("", "robin@wayne.com", "Teste.31122022")]
    [DataRow("superman superman superman superman superman superman", "superman@justiceleague.com", "Teste.31122022")]
    public void ShouldReturnInvalidCommandWhenNameIsInvalid(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "@wayne.com", "Teste.31122022")]
    [DataRow("robin", "robin@.com", "Teste.31122022")]
    [DataRow("superman", "supermanjusticeleague.com", "Teste.31122022")]
    public void ShouldReturnInvalidCommandWhenEmailIsInvalid(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@wayne.com", "31122022")]
    [DataRow("robin", "robin@wayne.com", "Teste31122022")]
    [DataRow("superman", "superman@justiceleague.com", "Tes.311")]
    public void ShouldReturnInValidCommandWhenPasswordIsInvalid(string name, string addres, string password)
    {
        _command.Name = name;
        _command.Email = addres;
        _command.Password = password;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }
}

