namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class RecoveryPasswordUserCommandTests
{
    private RecoveryPasswordUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com")]
    [DataRow("robin@wayne.com")]
    [DataRow("superman@justiceleague.com")]
    public void ShouldReturnValidEmailWhenAdressIsValid(string adress)
    {
        _command.Email = adress;
        _command.Validate();
        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman.com")]
    [DataRow("robin @wayne.com")]
    [DataRow("@justiceleague.com")]
    public void ShouldReturnInvalidEmailWhenAdressIsInvalid(string adress)
    {
        _command.Email = adress;
        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }
}

