namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class ConfirmRecoveryPasswordUserCommandTests
{
    [TestMethod]
    [DataTestMethod]
    [DataRow("81dc8173-3a83-4cfb-a62b-129db76a457d")]
    [DataRow("e7f5550c-b6cc-4923-a7b7-de1ff15ea071")]
    [DataRow("4d09252b-c733-4723-821c-7dd40af69236")]
    public void ShouldReturnValidCommandWhenGuidisValid(string id)
    {
        var command = new ConfirmRecoveryPasswordUserCommand(id);
        command.Validate();
        Assert.IsTrue(command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("")]
    [DataRow("e7f5550c-b6cc-4923-a7b7-de1ff15ea0711")]
    [DataRow("4d09252b-c733-4723-821c-7dd40af6923g")]
    public void ShouldReturnInvalidCommandWhenGuidIsInvalid(string id)
    {
        var command = new ConfirmRecoveryPasswordUserCommand(id);
        command.Validate();
        Assert.IsFalse(command.IsValid);
    }
}

