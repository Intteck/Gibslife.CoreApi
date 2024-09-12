using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gibs.Api.Services
{
    public class EmailService
    {
        private readonly SmtpClient _client;
        private readonly Settings.SMTPOptions _smtp;

        public EmailService(Settings settings)
        {
            _smtp = settings.SMTP;
            var (host, port) = ParseEndpoint(_smtp.ServerEndpoint);

            _client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(_smtp.Username, _smtp.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _smtp.UseSSL,
                Timeout = 300000
            };
        }

        public MailMessage CreateEmailMessage(string sendTo)
        {
            var message = new MailMessage
            {
                IsBodyHtml = false,
            };

            if (_smtp.FromAddress != null)
                message.From = new MailAddress(_smtp.FromAddress, _smtp.DisplayName);

            if (_smtp.ReplyToAddress != null)
                message.ReplyToList.Add( new MailAddress(_smtp.ReplyToAddress, _smtp.DisplayName));

            if (_smtp.SenderAddress != null)
                message.Sender = new MailAddress(_smtp.SenderAddress, _smtp.DisplayName);

            message.Bcc.Add("dejisys@idevworks.com");
            message.To.Add(sendTo);

            return message;
        }

        private static (string Host, int Port) ParseEndpoint(string serverEndpoint)
        {
            var port = 25;
            var parts = serverEndpoint.Split(':');

            if (parts.Length > 0)
                port = int.Parse(parts[1]);

            return (parts[0], port);
        }

        public Task SendEmailMessage(MailMessage message)
        {
            return _client.SendMailAsync(message);
        }
    }
}