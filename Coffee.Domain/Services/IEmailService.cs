namespace Coffee.Domain.Services;

public interface IEmailService
{
    bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Valmir de Lima",
        string fromEmail = "valmirblima7@gmail.com");
}