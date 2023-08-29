namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class UpdateUserCommandTests
{
    private readonly UpdateUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman")]
    [DataRow("robin")]
    [DataRow("superman")]
    public void ShouldReturnValidCommandWhenDatasAreValids(string name)
    {
        _command.Name = name;

        _command.Validate();
        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("ba")]
    [DataRow("")]
    [DataRow("superman superman superman superman superman superman")]
    public void ShouldReturnInvalidCommandWhenNameIsInvalid(string name)
    {
        _command.Name = name;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }
}

