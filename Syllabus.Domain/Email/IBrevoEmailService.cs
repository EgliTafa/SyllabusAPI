
namespace Syllabus.Domain.Services.Email
{
    public interface IBrevoEmailService
    {
        ValueTask<bool> SendEmailConfirmationAsync(string toEmail, string confirmationToken);
        ValueTask SendPasswordResetEmailAsync(string toEmail, string resetToken);
    }
}