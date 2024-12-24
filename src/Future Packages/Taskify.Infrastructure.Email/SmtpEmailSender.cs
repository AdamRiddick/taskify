namespace Taskify.Infrastructure.Email;

using Taskify.SharedKernel.Email;

public class SmtpEmailSender() : IEmailSender
{
    public Task SendEmailAsync(string to, string from, string subject, string body)
    {
        return Task.CompletedTask;
    }
}