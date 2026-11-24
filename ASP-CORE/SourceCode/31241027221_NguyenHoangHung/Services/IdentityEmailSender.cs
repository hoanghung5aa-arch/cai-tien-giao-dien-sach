using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Services;

public sealed class IdentityEmailSender : IEmailSender
{
    private readonly EmailSettings _settings;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<IdentityEmailSender> _logger;

    public IdentityEmailSender(
        IOptions<EmailSettings> settings,
        IHostEnvironment environment,
        ILogger<IdentityEmailSender> logger)
    {
        _settings = settings.Value;
        _environment = environment;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var userName = _settings.UserName?.Trim();
        var appPassword = _settings.AppPassword is null
            ? string.Empty
            : new string(_settings.AppPassword.Where(character => !char.IsWhiteSpace(character)).ToArray());

        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(appPassword))
        {
            if (_environment.IsDevelopment())
            {
                _logger.LogInformation(
                    "Email SMTP chưa cấu hình cho {Email}. Trang xác nhận local sẽ cung cấp link xác nhận.",
                    email);
                return;
            }

            throw new InvalidOperationException("Configure Email:UserName and Email:AppPassword before registering an account.");
        }

        if (string.IsNullOrWhiteSpace(_settings.Host) || _settings.Port is < 1 or > 65535)
        {
            throw new InvalidOperationException("Configure a valid Email:Host and Email:Port before sending email.");
        }

        using var message = new MailMessage
        {
            From = new MailAddress(userName, _settings.FromName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        message.To.Add(email);

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(userName, appPassword),
            Timeout = 15000
        };

        try
        {
            await client.SendMailAsync(message);
        }
        catch (Exception exception) when (_environment.IsDevelopment())
        {
            _logger.LogWarning(
                exception,
                "Không gửi được email xác nhận cho {Email} ở môi trường local.",
                email);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Không gửi được email xác nhận qua SMTP.");
            throw new InvalidOperationException("Email delivery failed. Check the SMTP host settings and try again.", exception);
        }
    }
}
