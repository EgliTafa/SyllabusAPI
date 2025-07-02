using brevo_csharp.Api;
using brevo_csharp.Model;
using Microsoft.Extensions.Options;
using Syllabus.Domain.Services.Email;
using Syllabus.Util.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Syllabus.Infrastructure.Services.Email
{
    public class BrevoEmailService : IBrevoEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly string _wwwrootPath;
        private static string? _resetPasswordTemplate;
        private static string? _emailConfirmationTemplate;
        private readonly ILogger<BrevoEmailService> _logger;

        public BrevoEmailService(IOptions<EmailOptions> emailOptions, IHostEnvironment env, ILogger<BrevoEmailService> logger)
        {
            _emailOptions = emailOptions.Value ?? throw new ArgumentNullException(nameof(emailOptions));
            _wwwrootPath = Path.Combine(env.ContentRootPath, "SyllabusAPI", "wwwroot");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private string LoadTemplate(string fileName)
        {
            // Try dev path first
            var devPath = Path.Combine(_wwwrootPath, fileName);
            if (System.IO.File.Exists(devPath))
                return System.IO.File.ReadAllText(devPath);

            // Fallback to ./wwwroot in current working dir (for Docker/publish)
            var cwdWwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            if (System.IO.File.Exists(cwdWwwroot))
                return System.IO.File.ReadAllText(cwdWwwroot);

            _logger.LogError("Email template not found: {DevPath} or {CwdWwwroot}", devPath, cwdWwwroot);
            throw new FileNotFoundException($"Email template not found: {devPath} or {cwdWwwroot}");
        }

        public async ValueTask SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Recipient email is required", nameof(toEmail));

            brevo_csharp.Client.Configuration.Default.AddApiKey("api-key", _emailOptions.ApiKey);

            var apiInstance = new TransactionalEmailsApi();
            var resetLink = $"{_emailOptions.ResetPasswordUrl}?token={Uri.EscapeDataString(resetToken)}";
            var logoUrl = string.IsNullOrEmpty(_emailOptions.BaseUrl) ? "http://localhost:5190/logo.png" : _emailOptions.BaseUrl.TrimEnd('/') + "/logo.png";

            if (_resetPasswordTemplate == null)
                _resetPasswordTemplate = LoadTemplate("reset-password-email.html");
            var html = _resetPasswordTemplate
                .Replace("{{logoUrl}}", logoUrl)
                .Replace("{{resetLink}}", resetLink);

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
                HtmlContent = html
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
                var confirmLink = $"{_emailOptions.EmailConfirmationUrl}?email={Uri.EscapeDataString(toEmail)}&token={Uri.EscapeDataString(confirmationToken)}";
                var logoUrl = string.IsNullOrEmpty(_emailOptions.BaseUrl) ? "http://localhost:5190/logo.png" : _emailOptions.BaseUrl.TrimEnd('/') + "/logo.png";

                if (_emailConfirmationTemplate == null)
                    _emailConfirmationTemplate = LoadTemplate("email-confirmation-email.html");
                var html = _emailConfirmationTemplate
                    .Replace("{{logoUrl}}", logoUrl)
                    .Replace("{{confirmLink}}", confirmLink);

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
                    HtmlContent = html
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