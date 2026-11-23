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
        if (string.IsNullOrWhiteSpace(_settings.UserName) || string.IsNullOrWhiteSpace(_settings.AppPassword))
        {
            _logger.LogInformation("Không gửi email xác nhận vì chưa cấu hình Gmail SMTP.");
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
                From = new MailAddress(_settings.UserName, _settings.FromName),
                Subject = $"Xác nhận đơn hàng #{order.Id} - Góc Phim Hưng",
                Body = body.ToString(),
                IsBodyHtml = false
            };
            message.To.Add(shipping.Email);

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.UserName, _settings.AppPassword)
            };
            cancellationToken.ThrowIfCancellationRequested();
            await client.SendMailAsync(message, cancellationToken);
            return true;
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Đơn #{OrderId} đã lưu nhưng email xác nhận chưa gửi được.", order.Id);
            return false;
        }
    }
}
