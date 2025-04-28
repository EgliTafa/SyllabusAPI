using brevo_csharp.Api;
using brevo_csharp.Model;
using Microsoft.Extensions.Options;
using Syllabus.Domain.Services.Email;
using Syllabus.Util.Options;

namespace Syllabus.Infrastructure.Services.Email
{
    public class BrevoEmailService : IBrevoEmailService
    {
        private readonly EmailOptions _emailOptions;

        public BrevoEmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async ValueTask SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            var apiInstance = new TransactionalEmailsApi();
            var resetLink = $"{_emailOptions.ResetPasswordUrl}?token={Uri.EscapeDataString(resetToken)}";

            var sendSmtpEmail = new SendSmtpEmail
            {
                Sender = new SendSmtpEmailSender
                {
                    Email = _emailOptions.SenderEmail,
                    Name = _emailOptions.SenderName
                },
                To = new List<SendSmtpEmailTo>
                {
                    new SendSmtpEmailTo { Email = toEmail }
                },
                Subject = "Reset Your Password",
                HtmlContent = $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>"
            };

            await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }

        public async ValueTask<bool> SendEmailConfirmationAsync(string toEmail, string confirmationToken)
        {
            try
            {
                var apiInstance = new TransactionalEmailsApi();
                var confirmLink = $"{_emailOptions.ResetPasswordUrl}?email={Uri.EscapeDataString(toEmail)}&token={Uri.EscapeDataString(confirmationToken)}";

                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender
                    {
                        Email = _emailOptions.SenderEmail,
                        Name = _emailOptions.SenderName
                    },
                    To = new List<SendSmtpEmailTo>
                    {
                        new SendSmtpEmailTo { Email = toEmail }
                    },
                    Subject = "Confirm Your Email",
                    HtmlContent = $"<p>Click <a href='{confirmLink}'>here</a> to confirm your email address.</p>"
                };

                await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
