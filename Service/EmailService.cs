using Duffl_career.Models;
using Duffl_career.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Duffl_career.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        // IOptions<EmailSettings> reads values from appsettings.json automatically
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // MimeMessage is the email object from MailKit
            var email = new MimeMessage();

            // FROM — who sends the email
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));

            // TO — who receives the email (the user's entered email)
            email.To.Add(MailboxAddress.Parse(toEmail));

            // Subject line of the email
            email.Subject = subject;

            // Body — HtmlBody means the email will render as HTML
            email.Body = new TextPart("html") { Text = body };

            // SmtpClient connects to Gmail's SMTP server and sends the email
            using var smtp = new SmtpClient();

            // Connect to Gmail SMTP with StartTls (secure connection on port 587)
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);

            // Authenticate with Gmail using email and app password
            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);

            // Send the email
            await smtp.SendAsync(email);

            // Disconnect after sending
            await smtp.DisconnectAsync(true);
        }
    }
}