namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class UpdateRecoveryPasswordUserCommandTests
{
    private UpdateRecoveryPasswordUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("93a6e2e3-2c55-476a-b6b7-ed731e02de88", "Test.123", "9e942bac")]
    [DataRow("301d5920-4325-454b-8238-c376bdbe0777", "Test.321", "148ec1ef")]
    [DataRow("98ae8c57-6899-4a48-91a5-7542734e82c1", "Test!123", "5f96f544")]
    public void ShouldReturnValidCommandWhenDatasAreValids(string id, string password, string recoveryPassword)
    {
        _command.Id = id;
        _command.Password = password;
        _command.RecoveryPassword = recoveryPassword;
        _command.Validate();

        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("", "Test.123", "9e942bac")]
    [DataRow("301d5920-4325-454b-8238-c376bdbe07778", "Test.321", "148ec1ef")]
    [DataRow("98ae8c57-6899-4a48-91a57-542734e82c1", "Test!123", "5f96f544")]
    public void ShouldReturnInvalidCommandWhenIDIsInvalid(string id, string password, string recoveryPassword)
    {
        _command.Id = id;
        _command.Password = password;
        _command.RecoveryPassword = recoveryPassword;
        _command.Validate();

        Assert.IsFalse(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("93a6e2e3-2c55-476a-b6b7-ed731e02de88", "", "9e942bac")]
    [DataRow("301d5920-4325-454b-8238-c376bdbe0777", "Test321", "148ec1ef")]
    [DataRow("98ae8c57-6899-4a48-91a5-7542734e82c1", "Tes!123", "5f96f544")]
    public void ShouldReturnInvalidCommandWhenPasswordIsInvalid(string id, string password, string recoveryPassword)
    {
        _command.Id = id;
        _command.Password = password;
        _command.RecoveryPassword = recoveryPassword;
        _command.Validate();

        Assert.IsFalse(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("93a6e2e3-2c55-476a-b6b7-ed731e02de88", "Test.123", "9e942bacc")]
    [DataRow("301d5920-4325-454b-8238-c376bdbe0777", "Test.321", "")]
    [DataRow("98ae8c57-6899-4a48-91a5-7542734e82c1", "Test!123", "5f96g544")]
    public void ShouldReturnInvalidCommandWhenRecoveryPasswordIsInvalid(string id, string password, string recoveryPassword)
    {
        _command.Id = id;
        _command.Password = password;
        _command.RecoveryPassword = recoveryPassword;
        _command.Validate();

        Assert.IsFalse(_command.IsValid);
    }
}

