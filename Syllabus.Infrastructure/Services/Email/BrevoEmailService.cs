using brevo_csharp.Api;
using brevo_csharp.Model;
using Microsoft.Extensions.Options;
using Syllabus.Domain.Services.Email;
using Syllabus.Util.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Syllabus.Infrastructure.Services.Email
{
    public class BrevoEmailService : IBrevoEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly string _wwwrootPath;
        private static string? _resetPasswordTemplate;
        private static string? _emailConfirmationTemplate;
        private ILogger<BrevoEmailService> _logger;

        public BrevoEmailService(IOptions<EmailOptions> emailOptions, IHostEnvironment env, ILogger<BrevoEmailService> logger)
        {
            _emailOptions = emailOptions.Value ?? throw new ArgumentNullException(nameof(emailOptions));
            // Use the content root path to find wwwroot in all environments
            _wwwrootPath = Path.Combine(env.ContentRootPath, "SyllabusAPI", "wwwroot");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Recipient email is required", nameof(toEmail));

            // Set the API key
            brevo_csharp.Client.Configuration.Default.AddApiKey("api-key", _emailOptions.ApiKey);

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
            new SendSmtpEmailTo(toEmail)
        },
                Subject = "Reset Your Password",
                HtmlContent = $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>"
            };

            await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }


        public async ValueTask<bool> SendEmailConfirmationAsync(string toEmail, string confirmationToken)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                return false;

            try
            {
                brevo_csharp.Client.Configuration.Default.AddApiKey("api-key", _emailOptions.ApiKey);

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
                        new SendSmtpEmailTo(toEmail)
                    },
                    Subject = "Confirm Your Email",
                    HtmlContent = $"<p>Click <a href='{confirmLink}'>here</a> to confirm your email address.</p>"
                };

                await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to send email confirmation to {Email}", toEmail);
                return false;
            }
        }
    }
}
