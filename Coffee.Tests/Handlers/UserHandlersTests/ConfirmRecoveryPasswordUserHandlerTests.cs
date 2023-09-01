namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class ConfirmRecoveryPasswordUserHandlerTests
{
    // False: Repository with actived users
    private MockUserRepository _repository = new(true);
    private readonly UserHandler _handler;
    private List<string> _guids = new();

    public ConfirmRecoveryPasswordUserHandlerTests()
    {
        _handler = new UserHandler(_repository, new MockTokenService(), new MockEmailService());

        _guids = GetIdUserRepository(_guids, _repository);
    }

    [TestMethod]
    [DataTestMethod]
    public async Task ShouldReturnTrueSuccessWhenIdIsValid()
    {
        foreach (var guid in _guids)
        {
            var command = new ConfirmRecoveryPasswordUserCommand(guid);

            var _result = (CommandResult)await _handler.HandleAsync(command);

            Assert.IsTrue(_result.Success);
        }
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("")]
    [DataRow("4244fc7d-8866-4591-b4c0-4d8167ddfc2d")]
    [DataRow("ae11e797-d314-4a8c-850-7cf8997060530")]
    [DataRow("b5601fd-1563a-4058-93af-b2f2a8ead73c")]
    public async Task ShouldReturnFalseSuccessWhenIdIsInvalid(string guid)
    {
        var command = new ActiveUserCommand(guid);

        var _result = (CommandResult)await _handler.HandleAsync(command);

        Assert.IsFalse(_result.Success);
    }

    private List<string> GetIdUserRepository(List<string> guids, MockUserRepository repository)
    {
        guids = new List<string>{
            repository.Users[0].Id.ToString(),
            repository.Users[1].Id.ToString(),
            repository.Users[2].Id.ToString(),
            repository.Users[3].Id.ToString()
        };
        return guids;
    }
}

