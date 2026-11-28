using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Authentication;

namespace _31241027221_NguyenHoangHung.Services;

internal static class EmailFailureDiagnostics
{
    public static string Classify(Exception exception)
    {
        for (var current = exception; current is not null; current = current.InnerException)
        {
            if (current is SocketException socketException)
            {
                return $"network-{socketException.SocketErrorCode}";
            }

            if (current is AuthenticationException)
            {
                return "tls-authentication";
            }
        }

        for (var current = exception; current is not null; current = current.InnerException)
        {
            if (current is SmtpException smtpException)
            {
                return $"smtp-{(int)smtpException.StatusCode}";
            }
        }

        return exception switch
        {
            TimeoutException => "timeout",
            OperationCanceledException => "cancelled",
            FormatException or ArgumentException => "invalid-address-or-message",
            InvalidOperationException => "invalid-configuration-or-client-state",
            _ => exception.GetType().Name
        };
    }
}
