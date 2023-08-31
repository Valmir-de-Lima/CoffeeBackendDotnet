namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class UpdatePasswordUserCommandTests
{
    private UpdatePasswordUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("Old.1234", "New.1234")]
    [DataRow("Old!4321", "New!4321")]
    [DataRow("Old#1234", "New#1234")]
    public void ShouldReturnValidCommandWhenDatasAreValids(string oldPassword, string newPassword)
    {
        _command.OldPassword = oldPassword;
        _command.NewPassword = newPassword;

        _command.Validate();
        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("", "New.1234")]
    [DataRow("Old!432", "New!4321")]
    [DataRow("Old12345", "New#1234")]
    public void ShouldReturnInvalidCommandWhenOldPasswordIsInvalid(string oldPassword, string newPassword)
    {
        _command.OldPassword = oldPassword;
        _command.NewPassword = newPassword;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }


    [TestMethod]
    [DataTestMethod]
    [DataRow("Old.1234", "")]
    [DataRow("Old!4321", "New!432")]
    [DataRow("Old#1234", "New12345")]
    public void ShouldReturnInvalidCommandWhenNewPasswordIsInvalid(string oldPassword, string newPassword)
    {
        _command.OldPassword = oldPassword;
        _command.NewPassword = newPassword;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }
}

