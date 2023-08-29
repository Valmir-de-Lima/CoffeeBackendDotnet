using Coffee.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
[TestCategory("Queries")]
public class UserQueriesTests
{
    private MockUserRepository _repository;

    public UserQueriesTests()
    {
        _repository = new MockUserRepository();
    }


    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com")]
    [DataRow("robin@wayne.com")]
    [DataRow("superman@justiceleague.com")]
    public void ShouldReturnOneUserWhenEmailIsValid(string adress)
    {
        var result = _repository.Users.AsQueryable().Where(UserQueries.GetByEmail(new Email(adress)));
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman.com")]
    [DataRow("robin @wayne.com")]
    [DataRow("@justiceleague.com")]
    public void ShouldReturnZeroWhenEmailIsInvalid(string adress)
    {
        var result = _repository.Users.AsQueryable().Where(UserQueries.GetByEmail(new Email(adress)));
        Assert.AreEqual(0, result.Count());
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman@wayne.com")]
    [DataRow("robin@wayne.com")]
    [DataRow("superman@justiceleague.com")]
    public void ShouldReturnNotNullWhenEmailExists(string adress)
    {
        var result = _repository.Users.AsQueryable().FirstOrDefault(UserQueries.ExistsEmail(new Email(adress)));
        Assert.AreNotEqual(result, null);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman.com")]
    [DataRow("robin @wayne.com")]
    [DataRow("@justiceleague.com")]
    public void ShouldReturnNullWhenEmailDontExists(string adress)
    {
        var result = _repository.Users.AsQueryable().FirstOrDefault(UserQueries.ExistsEmail(new Email(adress)));
        Assert.AreEqual(result, null);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-wayne-com")]
    [DataRow("robin-wayne-com")]
    [DataRow("superman-justiceleague-com")]
    public void ShouldReturnNotNullWhenLinkExists(string link)
    {
        var result = _repository.Users.AsQueryable().FirstOrDefault(UserQueries.GetByLink(link));
        Assert.AreNotEqual(result, null);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman-com")]
    [DataRow("robin-wayne")]
    [DataRow("-justiceleague-com")]
    public void ShouldReturnNullWhenLinkDontExists(string link)
    {
        var result = _repository.Users.AsQueryable().FirstOrDefault(UserQueries.GetByLink(link));
        Assert.AreEqual(result, null);
    }

}

