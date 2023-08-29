namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class GetUserCommandTests
{
    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com")]
    [DataRow("robin-wayne-com")]
    [DataRow("superman-league-com")]
    public void ShouldReturnValidCommandWhenLinkisValid(string id)
    {
        var command = new GetUserCommand(id);
        command.Validate();
        Assert.IsTrue(command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("   ")]
    public void ShouldReturnInvalidCommandWhenLinkIsInvalid(string id)
    {
        var command = new GetUserCommand(id);
        command.Validate();
        Assert.IsFalse(command.IsValid);
    }
}

