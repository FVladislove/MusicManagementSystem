using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MusicManagementSystem.Models.NotMapped.SecretsModels;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MusicManagementSystem.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        public SendGridModel SendGridOptions { get; }
        public EmailSender(IOptions<SendGridModel> sendGridOptions,
            ILogger<EmailSender> logger)
        {
            SendGridOptions = sendGridOptions.Value;
            _logger = logger;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(SendGridOptions.Key))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(SendGridOptions.Key, subject, htmlMessage, mailTo);
        }
        public async Task Execute(string apiKey, string subject, string htmlMessage, string mailTo)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(SendGridOptions.MailFrom),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(mailTo));

            // disable click tracking
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                ? $"Email to {mailTo} queued successfully"
                : $"Failure Email to {mailTo}");
        }
    }
}
