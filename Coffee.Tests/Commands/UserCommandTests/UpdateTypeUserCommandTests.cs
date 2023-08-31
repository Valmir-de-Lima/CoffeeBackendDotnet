namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class UpdateTypeUserCommandTests
{
    private UpdateTypeUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", EType.Manager)]
    [DataRow("catwoman@wayne.com", EType.Barista)]
    [DataRow("robin@wayne.com", EType.Deliveryman)]
    [DataRow("superman@justiceleague.com", EType.Customer)]
    public void ShouldReturnValidCommandWhenDatasAreValids(string adress, EType type)
    {
        _command.Email = adress;
        _command.Type = (int)type;
        _command.Validate();

        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("@wayne.com", EType.Manager)]
    [DataRow("catwoman.com", EType.Barista)]
    [DataRow("robin@.com", EType.Deliveryman)]
    [DataRow("superman justiceleague.com", EType.Customer)]
    public void ShouldReturnInvalidCommandWhenEmailIsInvalid(string adress, EType type)
    {
        _command.Email = adress;
        _command.Type = (int)type;
        _command.Validate();

        Assert.IsFalse(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com", -1)]
    [DataRow("catwoman@wayne.com", 4)]
    [DataRow("robin@wayne.com", 5)]
    [DataRow("superman@justiceleague.com", -2)]
    public void ShouldReturnInvalidCommandWhenTypeIsinvalid(string adress, EType type)
    {
        _command.Email = adress;
        _command.Type = (int)type;
        _command.Validate();

        Assert.IsFalse(_command.IsValid);
    }
}

