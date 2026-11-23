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
        if (string.IsNullOrWhiteSpace(_settings.UserName) || string.IsNullOrWhiteSpace(_settings.AppPassword))
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

        using var message = new MailMessage
        {
            From = new MailAddress(_settings.UserName, _settings.FromName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        message.To.Add(email);

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_settings.UserName, _settings.AppPassword)
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
    }
}
