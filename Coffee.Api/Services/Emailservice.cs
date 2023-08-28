using System.Net;
using System.Net.Mail;
using Coffee.Domain.Services;


namespace Coffee.Api.Services;

public class EmailService : IEmailService
{
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Valmir de Lima",
        string fromEmail = "valmirblima7@gmail.com")
    {
        // Todas as informacoes necessarias para criar o servico de email encontra-se no appsettings.json -> Configuration.cs
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);
        smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; // Metodo de entrega: internet

        // Port 587, habilitar porta segura
        smtpClient.EnableSsl = true;

        // Criar a mensagem de email
        var mail = new MailMessage();

        mail.From = new MailAddress(fromEmail, fromName); // Remetente
        mail.To.Add(new MailAddress(toEmail, toName)); // Destinatario(s)
        mail.Subject = subject; // Assunto
        mail.Body = body; // Corpo do email
        mail.IsBodyHtml = true; // Pode-se enviar no corpo da mensagem tags html para formatar a mensagem

        try
        {
            smtpClient.Send(mail);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}