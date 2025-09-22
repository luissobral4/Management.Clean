using Management.Clean.Application.Models.Emails;

namespace Management.Clean.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailMessage email);
}