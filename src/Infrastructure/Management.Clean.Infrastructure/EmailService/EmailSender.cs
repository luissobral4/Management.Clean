using Management.Clean.Application.Contracts.Email;
using Management.Clean.Application.Models.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Management.Clean.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private EmailSettings _emailSettings { get; }

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(EmailMessage email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress(_emailSettings.FromAddress, _emailSettings.FromName);

        var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        var response = await client.SendEmailAsync(msg);

        return response.IsSuccessStatusCode; 
    }
}