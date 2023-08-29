namespace Coffee.Tests.Commands.UserCommandTests;

[TestClass]
[TestCategory("Commands")]
public class RefreshLoginUserCommandTests
{
    private readonly RefreshLoginUserCommand _command = new();

    [TestMethod]
    [DataTestMethod]
    [DataRow("token1", "refreshToken1")]
    [DataRow("token2", "refreshToken2")]
    [DataRow("token3", "refreshToken3")]
    public void ShouldReturnValidCommandWhenDatasAreValids(string token, string refreshToken)
    {
        _command.Token = token;
        _command.RefreshToken = refreshToken;

        _command.Validate();
        Assert.IsTrue(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("", "refreshToken1")]
    [DataRow("", "refreshToken2")]
    [DataRow("", "refreshToken3")]
    public void ShouldReturnInValidCommandWhenTokenIsInValid(string token, string refreshToken)
    {
        _command.Token = token;
        _command.RefreshToken = refreshToken;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("token1", "")]
    [DataRow("token2", "")]
    [DataRow("token3", "")]
    public void ShouldReturnInValidCommandWhenRefreshTokenIsInValid(string token, string refreshToken)
    {
        _command.Token = token;
        _command.RefreshToken = refreshToken;

        _command.Validate();
        Assert.IsFalse(_command.IsValid);
    }
}

