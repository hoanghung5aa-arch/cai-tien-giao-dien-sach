using System.Text.Json;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Extension;

public static class SessionExtensions
{
    private const string CartKey = "MovieCart";

    public static List<CartItem> GetCart(this ISession session)
    {
        var json = session.GetString(CartKey);
        return string.IsNullOrWhiteSpace(json)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    public static void SaveCart(this ISession session, IEnumerable<CartItem> items)
        => session.SetString(CartKey, JsonSerializer.Serialize(items));

    public static void ClearCart(this ISession session)
        => session.Remove(CartKey);
}
