using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Services;

public sealed class IdentityEmailSender : IEmailSender
{
    private readonly EmailSettings _settings;
    private readonly ILogger<IdentityEmailSender> _logger;

    public IdentityEmailSender(
        IOptions<EmailSettings> settings,
        ILogger<IdentityEmailSender> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var userName = _settings.UserName?.Trim();
            var appPassword = _settings.AppPassword is null
                ? string.Empty
                : new string(_settings.AppPassword.Where(character => !char.IsWhiteSpace(character)).ToArray());

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(appPassword))
            {
                throw new InvalidOperationException("SMTP credentials are not configured.");
            }

            if (string.IsNullOrWhiteSpace(_settings.Host) || _settings.Port is < 1 or > 65535)
            {
                throw new InvalidOperationException("SMTP host or port is invalid.");
            }

            using var message = new MailMessage
            {
                From = new MailAddress(userName, _settings.FromName),
                Subject = subject,
                SubjectEncoding = Encoding.UTF8,
                Body = htmlMessage,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            message.To.Add(email);

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(userName, appPassword),
                Timeout = 15000
            };

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            try
            {
                await client.SendMailAsync(message, timeout.Token);
            }
            catch (OperationCanceledException) when (timeout.IsCancellationRequested)
            {
                throw new TimeoutException("SMTP submission exceeded its time limit.");
            }
        }
        catch (Exception exception)
        {
            var failureCategory = EmailFailureDiagnostics.Classify(exception);
            _logger.LogError("Confirmation email submission failed ({FailureCategory}).", failureCategory);
            throw new InvalidOperationException("Email delivery failed. Check the SMTP settings and connectivity, then try again.");
        }
    }
}
