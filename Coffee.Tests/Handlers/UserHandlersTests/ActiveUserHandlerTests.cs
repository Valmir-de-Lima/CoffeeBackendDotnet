namespace Coffee.Tests.Handlers.UserHandlerTests;

[TestClass]
[TestCategory("Handlers")]
public class ActiveUserHandlersTests
{
    private readonly MockUserRepository _repository = new(false);
    private readonly UserHandler _handler;
    private List<string> _guids;

    public ActiveUserHandlersTests()
    {
        _handler = new UserHandler(_repository, new MockTokenService(), new MockEmailService());

        _guids = new List<string>{
            _repository.Users[0].Id.ToString(),
            _repository.Users[1].Id.ToString(),
            _repository.Users[2].Id.ToString(),
            _repository.Users[3].Id.ToString()
        };
    }

    [TestMethod]
    [DataTestMethod]
    public async Task ShouldReturnTrueSuccessWhenIdIsValid()
    {
        foreach (var guid in _guids)
        {
            var command = new ActiveUserCommand(guid);

            var _result = (CommandResult)await _handler.HandleAsync(command);

            Assert.IsTrue(_result.Success);
        }
    }
}

