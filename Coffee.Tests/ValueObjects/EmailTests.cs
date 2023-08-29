namespace Coffee.Tests.ValueObjects;

[TestClass]
[TestCategory("ValueObjects")]
public class EmailTests
{
    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com")]
    [DataRow("robin@wayne.com")]
    [DataRow("superman@justiceleague.com")]
    public void ShouldReturnValidEmailWhenAdressIsValid(string adress)
    {
        var email = new Email(adress);
        Assert.IsTrue(email.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman.com")]
    [DataRow("robin @wayne.com")]
    [DataRow("@justiceleague.com")]
    public void ShouldReturnInvalidEmailWhenAdressIsInvalid(string adress)
    {
        var email = new Email(adress);
        Assert.IsFalse(email.IsValid);
    }
}

