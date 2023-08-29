namespace Coffee.Tests.Entities;

[TestClass]
[TestCategory("Entities")]
public class UserTests
{
    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@wayne.com", "Teste.31122022", EType.Manager)]
    [DataRow("robin", "robin@wayne.com", "Teste.31122022", EType.Deliveryman)]
    [DataRow("superman", "superman@justiceleague.com", "Teste.31122022", EType.Customer)]
    public void ShouldReturnValidUserWhenDatasAreValids(string name, string addres, string password, EType type)
    {
        var user = new User(name, new Email(addres), new Password(password), type);
        Assert.IsTrue(user.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("ba", "batman@wayne.com", "Teste.31122022", EType.Manager)]
    [DataRow("", "robin@wayne.com", "Teste.31122022", EType.Deliveryman)]
    [DataRow("superman superman superman superman superman superman", "superman@justiceleague.com", "Teste.31122022", EType.Customer)]
    public void ShouldReturnInvalidUserWhenNameIsInvalid(string name, string addres, string password, EType type)
    {
        var user = new User(name, new Email(addres), new Password(password), type);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "@wayne.com", "Teste.31122022", EType.Manager)]
    [DataRow("robin", "robin@.com", "Teste.31122022", EType.Deliveryman)]
    [DataRow("superman", "supermanjusticeleague.com", "Teste.31122022", EType.Customer)]
    public void ShouldReturnInvalidUserWhenEmailIsInvalid(string name, string addres, string password, EType type)
    {
        var user = new User(name, new Email(addres), new Password(password), type);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman", "batman@wayne.com", "Teste.31122022", -1)]
    [DataRow("robin", "robin@wayne.com", "Teste.31122022", 5)]
    [DataRow("superman", "superman@justiceleague.com", "Teste.31122022", 4)]
    public void ShouldReturnInvalidUserWhenTypeIsInvalid(string name, string addres, string password, EType type)
    {
        var user = new User(name, new Email(addres), new Password(password), type);
        Assert.IsFalse(user.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("batman")]
    [DataRow("robin")]
    [DataRow("superman")]
    public void ShouldReturnValidUserWhenDatasUpdateAreValids(string name)
    {
        var user = new User("Flash", new Email("hero@justiceleague.com"), new Password("Teste.31122022"), EType.Customer);
        user.UpdateName(name);
        Assert.IsTrue(user.IsValid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("ba")]
    [DataRow("")]
    [DataRow("superman superman superman superman superman superman")]
    public void ShouldReturnInvalidUserWhenNameUpdateisInvalid(string name)
    {
        var user = new User("Flash", new Email("hero@justiceleague.com"), new Password("Teste.31122022"), EType.Customer);
        user.UpdateName(name);
        Assert.IsFalse(user.IsValid);
    }

    // [TestMethod]
    // [DataTestMethod]
    // [DataRow("batman", "123456", -1)]
    // [DataRow("robin", "123456", 3)]
    // [DataRow("superman", "123456", 4)]
    // public void ShouldReturnInvalidUserWhenTypeUpdateisInvalid(string name, string password, EType type)
    // {
    //     var user = new User("Flash", new Email("hero@justiceleague.com"), "123456", EType.Customer);
    //     user.Update(name, password);
    //     Assert.IsFalse(user.IsValid);
    // }
}

