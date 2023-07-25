using System.Net.Mail;
using API.Contracts;

namespace API.Utilities.Handlers;

public class EmailHandler : IEmailHandler
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _fromEmailAddress;

    public EmailHandler(string smtpServer, int smtpPort, string fromEmailAddress)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _fromEmailAddress = fromEmailAddress;
    }

    public void SendEmail(string toEmail, string subject, string htmlMessage)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_fromEmailAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(toEmail));

        using var client = new SmtpClient(_smtpServer, _smtpPort);
        client.Send(message);
    }
}
