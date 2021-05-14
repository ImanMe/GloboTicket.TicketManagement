using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Models.Mail;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail; // using Microsoft.Extensions.Logging;

namespace GloboTicket.TicketManagement.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings EmailSettings { get; }
        // public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> mailSettings/* ILogger<EmailService> logger*/)
        {
            EmailSettings = mailSettings.Value;
            //_logger = logger;
        }

        public async Task<bool> SendEmail(Application.Models.Mail.Email email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = EmailSettings.FromAddress,
                Name = EmailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            // _logger.LogInformation("Email sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            // _logger.LogError("Email sending failed");

            return false;
        }
    }
}