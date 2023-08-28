using Coffee.Domain.Commands.UserCommands;

namespace Coffee;

public static class Configuration
{
    public const string MANAGER = "Manager";
    public const string EMPLOYEE = "Employee";
    public const string CUSTUMER = "Custumer";
    public static string JwtKey = "";
    public static SmtpConfiguration Smtp = new();

    public static CreateManagerCommand CreatePrimaryManager(IConfiguration config, string urlOfSite)
    {
        var createManagerCommand = new CreateManagerCommand();

        config.GetSection("Manager").Bind(createManagerCommand);
        createManagerCommand.SetUrlOfSite(urlOfSite);

        return createManagerCommand;
    }

    public class SmtpConfiguration
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 25;
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}