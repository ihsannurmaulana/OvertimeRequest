using API.Contracts;
using System.Net;
using System.Net.Mail;

namespace API.Utilities.Handlers;

public class EmailHandler : IEmailHandler
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _fromEmailAddress;
    private readonly string _fromEmailPassword;

    public EmailHandler(string smtpServer, int smtpPort, string fromEmailAddress, string fromEmailPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _fromEmailAddress = fromEmailAddress;
        _fromEmailPassword = fromEmailPassword;
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

        /* using var client = new SmtpClient(_smtpServer, _smtpPort);*/
        var client = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            //UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_fromEmailAddress, _fromEmailPassword),
            EnableSsl = true,
        };
        client.Send(message);
    }
}