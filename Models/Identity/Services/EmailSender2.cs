using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectItiTeam.Models.Identity.Services
{
    public class EmailSender2 : IEmailSender
    {
        private readonly string smptServer;
        private readonly int smtPort;
        private readonly string fromEmailAddress;

        public EmailSender2(string SmptServer,int smtPort,string fromEmailAddress)
        {
            smptServer = SmptServer;
            this.smtPort = smtPort;
            this.fromEmailAddress = fromEmailAddress;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using (var client = new SmtpClient(smptServer, smtPort))
            {
                client.Send(message);
            }
            return Task.CompletedTask;
        }
    }
}
