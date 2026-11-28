using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Services;

public interface IOrderEmailService
{
    Task<bool> TrySendConfirmationAsync(
        HoaDon order,
        ShippingDetail shipping,
        IReadOnlyDictionary<int, Movie> movies,
        CancellationToken cancellationToken = default);
}

public sealed class OrderEmailService : IOrderEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<OrderEmailService> _logger;

    public OrderEmailService(IOptions<EmailSettings> settings, ILogger<OrderEmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<bool> TrySendConfirmationAsync(
        HoaDon order,
        ShippingDetail shipping,
        IReadOnlyDictionary<int, Movie> movies,
        CancellationToken cancellationToken = default)
    {
        var userName = _settings.UserName?.Trim();
        var appPassword = _settings.AppPassword is null
            ? string.Empty
            : new string(_settings.AppPassword.Where(character => !char.IsWhiteSpace(character)).ToArray());

        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(appPassword))
        {
            _logger.LogInformation("Order email submission skipped (smtp-credentials-not-configured); the order remains saved.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(_settings.Host) || _settings.Port is < 1 or > 65535)
        {
            _logger.LogWarning("Order email submission skipped (smtp-host-or-port-invalid); the order remains saved.");
            return false;
        }

        try
        {
            var body = new StringBuilder()
                .AppendLine($"Chào {shipping.FullName},")
                .AppendLine()
                .AppendLine($"Góc Phim Hưng đã nhận đơn hàng #{order.Id}.")
                .AppendLine()
                .AppendLine("Chi tiết đơn hàng:");

            foreach (var item in order.Items)
            {
                var title = movies.TryGetValue(item.MovieId, out var movie) ? movie.Title : $"Phim #{item.MovieId}";
                body.AppendLine($"- {title} × {item.Quantity} | {item.UnitPrice:N0} VNĐ/đĩa | Thành tiền: {item.UnitPrice * item.Quantity:N0} VNĐ");
            }

            body.AppendLine()
                .AppendLine($"Tổng tiền: {order.Total:N0} VNĐ")
                .AppendLine($"Địa chỉ giao hàng: {shipping.Address}")
                .AppendLine($"Số điện thoại: {shipping.Mobile}")
                .AppendLine($"Ngày giao dự kiến: {shipping.ReleaseDate:dd/MM/yyyy}")
                .AppendLine()
                .Append("Cảm ơn bạn đã ủng hộ Góc Phim Hưng.");

            using var message = new MailMessage
            {
                From = new MailAddress(userName, _settings.FromName),
                Subject = $"Xác nhận đơn hàng #{order.Id} - Góc Phim Hưng",
                Body = body.ToString(),
                IsBodyHtml = false
            };
            message.To.Add(shipping.Email);

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(userName, appPassword),
                Timeout = 15000
            };
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeout.CancelAfter(TimeSpan.FromSeconds(15));
            try
            {
                await client.SendMailAsync(message, timeout.Token);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested && timeout.IsCancellationRequested)
            {
                throw new TimeoutException("SMTP submission exceeded its time limit.");
            }

            return true;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Order email submission was canceled (order {OrderId}); the order remains saved.", order.Id);
            return false;
        }
        catch (Exception exception)
        {
            var failureCategory = EmailFailureDiagnostics.Classify(exception);
            _logger.LogWarning(
                "Order email submission failed for order {OrderId} ({FailureCategory}); the order remains saved.",
                order.Id,
                failureCategory);
            return false;
        }
    }
}
